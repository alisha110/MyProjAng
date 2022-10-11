import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ManagmentService } from '../managment.service';
import { IUser } from '../Models/IUser';
import Swal from 'sweetalert2';
import { IError } from '../Models/IError';
import { HttpErrorResponse } from '@angular/common/http';
declare var window: any;


@Component({
  selector: 'app-register-new-user',
  templateUrl: './register-new-user.component.html',
  styleUrls: ['./register-new-user.component.scss']
})
export class RegisterNewUserComponent implements OnInit {

  constructor(private service: ManagmentService, private route: ActivatedRoute, private router: Router) { }
  currentUser: IUser= new IUser();
  //modalRef: BsModalRef;
  formModal: any;
  deleteBlockStyle = "none";
  showModalBox: boolean= false;
  ngOnInit(): void {
    this.service.autoLogin();
    if(!ManagmentService.isAuthentication){
      this.router.navigateByUrl('/Login');
      return;
    }

    let y = this.route.snapshot.paramMap.get('id');
    if(y != null)
    {
      this.service.getUser(y).subscribe(
        (response) => {     
          let Tmp = this.currentUser.UserName;
          this.currentUser = response;
          this.currentUser.UserName = Tmp;
          //this.currentUser.ouplace = 'LDAP://OU=test-rabbani,OU=e-users,OU=Employee,OU=ITC-MUMS,OU=Data Center,OU=BranchOffice,DC=mums,DC=ac,DC=ir'
        },
        error => {    
            this.service.ErrorManagment(error);
        }
      );
    }
    if(y != null){
      this.currentUser.UserName = 'Please Wait ....';
      this.service.getProposalUserName(y)
        .subscribe(
          (response)=>{
            this.currentUser.UserName = response;
          },
          (error)=>{
            //console.log(error);
            this.service.ErrorManagment(error);
            this.currentUser.UserName = '';
          }
        );
      }
    }

  public backClick(event: any){
    //this.deleteBlockStyle = "block";
    this.router.navigateByUrl('/list-new-users');
  }

  public RegisterClick(event: any){
    this.service.createUser(this.currentUser)
    .subscribe(
      (response) => { 
        this.currentUser.password = response;
        Swal.fire({
          title : 'ثبت با موفقیت انجام شد!', 
          html : '<h4>UserName: ' + this.currentUser.UserName + '</h4><h4>Password: ' + this.currentUser.password + '</h4>', 
          icon :'success',
        }).then((result)=>{
          if(result.isConfirmed){
            this.router.navigateByUrl('/list-new-users');
          }
        });

      },
      (error) => { 
        let t = <HttpErrorResponse> error;
        if(t.error.indexOf('The object already exists.') >= 0){
          Swal.fire('نام کاربری در اکتیودایرکتوری تکراری می باشد.')
          //console.log('کاربر تکراری است');
        }else{
          Swal.fire('در زمان ثبت خطایی رخ داده است')
        }
        
      });
  }

  public CloseModal(event:any){
    this.deleteBlockStyle = "none";
    this.router.navigateByUrl('/list-new-users');
  }

  public usernameonChange(event: any){
    this.currentUser.UserName = event;   
  }
}
