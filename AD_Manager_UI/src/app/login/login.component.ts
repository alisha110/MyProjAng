import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, isEmpty } from 'rxjs';
import { ManagmentService } from '../managment.service';
import { IError } from '../Models/IError';
import { IUserToken } from '../Models/IUser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  ShowError : string = '';

  constructor(private service: ManagmentService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.service.autoLogin();
    if(ManagmentService.isAuthentication)
    { 
      let y = this.route.snapshot.paramMap.get('RDPage');
      console.log(y);
      if(y == 'UserList')
        this.router.navigateByUrl('/list-new-users');
      else
        this.router.navigateByUrl('/');
      
    }
  }
  public loginClick(event: any){
    const UserName = <HTMLInputElement>document.getElementById('UserName')
    const Password = <HTMLInputElement>document.getElementById('Password')
    if(UserName && Password ) {
      if(!UserName.value || UserName.value.length === 0){
        this.ShowError = "نام کاربری را وارد کنید.";
        return;
      }
      if(!Password.value || Password.value.length === 0){
        this.ShowError = "رمز عبور را وارد کنید.";
        return;
      }
      
      this.service.login(UserName.value, Password.value)
        .subscribe(
          (data: any) => { 
            console.log(data);
            this.service.handleAuthentication(UserName.value, data);
            this.router.navigateByUrl('/');
          },
          (error: any) => { 
            console.log('Error:');
            
            console.error(error);
            let s  = <HttpErrorResponse> error;
            this.ShowError = (<IError>s.error).errorMessages;
            //alert((<IError>s.error).errorMessages);
          });
    }
  }

}
