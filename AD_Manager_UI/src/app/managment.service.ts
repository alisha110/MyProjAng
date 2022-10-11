import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, retry, tap } from 'rxjs';
import { IUser, IUserToken } from './Models/IUser';
import { Router } from '@angular/router';
import { IReport } from './Models/Report';
@Injectable({
  providedIn: 'root'
})
export class ManagmentService {
  public authUser = new BehaviorSubject<IUserToken|null>(null);
  public Token: string = '';
  public static isAuthentication: boolean= false;
  public  sessionName: string = "_Web_UserData_";
  readonly APIUrl: string = "http://localhost:5041";
  errorMsg: string | undefined;
  constructor(private http: HttpClient, private router: Router) { }


  getHeader(): HttpHeaders{
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Authorization', 'Bearer ' + this.Token);
    headers = headers.append('responseType', 'text' as 'json');
    //console.log(headers);
    return headers;
  }

  getUserList(): Observable<IUser[]> {
    let headers = this.getHeader();
    
    return this.http.post<IUser[]>(this.APIUrl + '/api/AD/GetUserList', '', {headers});;
  }

  getReportList(): Observable<IReport[]> {
    let headers = this.getHeader();
    
    return this.http.post<IReport[]>(this.APIUrl + '/api/AD/GetReport', '', {headers});;
  }

  getTestUserList() {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.Token });
    return this.http.post(this.APIUrl + '/api/AD/GetUserList', '', {headers});
    // if(x)
    //   return x.result;

    // return this.http.post(this.APIUrl + '/api/AD/GetUserList', '', {headers});
  }

  getOldUserList(): Observable<IUser[]> {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.Token });
    
    return  this.http.post<IUser[]>(this.APIUrl + '/api/AD/GetOldUserList', '', {headers});
  }

  getUser(userID: string): Observable<IUser> {
    //console.log("Service Open");
    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.Token });
    return this.http.post<IUser>(this.APIUrl + '/api/AD/GetUser?UserID=' + userID, '', {headers});
  }

  getProposalUserName(userID: string): Observable<string> {
    //console.log("Service Open");
    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.Token });
    return this.http.post<string>(this.APIUrl + '/api/AD/GetProposalUserName?UserID=' + userID, '',
      {headers, responseType: 'text' as 'json'});
  }

  setActiveProposalUserName(userID: string): Observable<string> {
    //console.log("Service Open");
    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.Token });
    console.log(headers);
    
    return this.http.post<string>(this.APIUrl + '/api/AD/ActiveDeactiveUserInAD?UserID=' + userID, '',
      {headers, responseType: 'text' as 'json'});
  }


  createUser(newUser: IUser): Observable<string>{
    
    var raw = JSON.stringify({
      "firstName": newUser.firstName,
      "lastName": newUser.lastName,
      "nationalCode": newUser.nationalCode,
      "grade": newUser.grade,
      "jobStringName": newUser.jobStringName,
      "workplace": newUser.workplace,
      "unit_khedmat": newUser.unit_khedmat,
      "shaghel": false,
      "halat_eshteghal": newUser.halat_eshteghal,
      "personNo": newUser.personNo,
      "personCode": newUser.personCode,
      "major": "",
      "UserName": newUser.UserName,
      "password": "",
      "email": newUser.UserName + "@mums.ac.ir",
      "ouplace": newUser.ouplace
    });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.Token });
    return this.http.post<string>(this.APIUrl + '/api/AD/CreateUserInAD', raw, {headers, responseType: 'text' as 'json'});
  }

  ActiveDeactiveUser(newUser: IUser): Observable<string>{
    console.log(newUser.personCode);
    
    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.Token });
    return this.http.post<string>(this.APIUrl + '/api/AD/ActiveDeactiveUserInAD?userID=' + newUser.personCode, {headers, responseType: 'text' as 'json'});
  }
  SetActiveDeactiveUser(newUser: IUser): Observable<string>{
    console.log(newUser.personCode);
    let headers = this.getHeader();
    console.log(headers);
    return this.http.post<string>(this.APIUrl + '/api/AD/GetProposalUserName?UserID=' + newUser.personCode, 
        {headers});
  }

  login(UserName: string, Password: string): any{
    var raw = JSON.stringify({
      "userName": UserName,
      "password": Password,
      "captcha": "",
      "_token": "",
      "expiresIn": "",
      "role": "",
      "gtUser": true,
      "permitions": [""]
    });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.APIUrl + '/Login', raw, {headers, responseType: 'text' as 'json'});
  }

  autoLogin() {
    //console.log('Auto Login');
    //console.log(localStorage.getItem('_Web_UserData_'));
    
    const userData: {
      userName: string;
      token: string;
      tokenExpirationDate: string;
    } = JSON.parse(localStorage.getItem('_Web_UserData_') || 'null');
    //console.log('userdata: ', userData);
    
    if (!userData) {
      return;
    }

    //console.log(userData.token);
    this.Token = userData.token;
    const loadedUser = new IUserToken(
      userData.userName,
      userData.token,
      new Date(userData.tokenExpirationDate)
    );
    if (loadedUser.token) {
      // console.log('loadedUser.token');
      this.authUser.next(loadedUser);
      const expirationDuration =
        new Date(userData.tokenExpirationDate).getTime() -
        new Date().getTime();
      ManagmentService.isAuthentication = true;
      //this.autoLogout(expirationDuration);

    }
    //console.log('Login: ', ManagmentService.isAuthentication);
    
  }
  logout() {
    //console.log("LogOut");
    this.Token = '';
    this.authUser.next(null);
    this.router.navigate(["/"]);
    localStorage.removeItem(this.sessionName);
    // if (this.tokenExpirationTimer) {
    //   clearTimeout(this.tokenExpirationTimer);
    // }
    // this.tokenExpirationTimer = null;
    ManagmentService.isAuthentication = false;
  }

  public handleAuthentication(loginuser: string, token: string) {
    this.Token = token;
    const expirationDate = new Date(new Date().getTime() + (6 * 60 * 60 * 1000));
    const user = new IUserToken(loginuser, token, expirationDate);
    this.authUser.next(user);
    //console.log(loginuser);
    //this.autoLogout(expiresIn);
    localStorage.setItem(this.sessionName, JSON.stringify(user));
    ManagmentService.isAuthentication = true;
  }

  public ErrorManagment(error : any){
    let t = <HttpErrorResponse> error;
    //console.error('error:' , t.error);

    if(t.statusText == 'Unauthorized'){
      this.logout();
      this.router.navigateByUrl('/Login');
    }
  }

}


