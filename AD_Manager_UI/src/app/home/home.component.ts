import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ManagmentService } from '../managment.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    if(!ManagmentService.isAuthentication){
      this.router.navigateByUrl('/Login');
      return;
    }
  }

  public newUserClick(){
    this.router.navigateByUrl('/list-new-users');
  }

  public SetActivationClick(){
    this.router.navigateByUrl('/Set-activation-user');
  }

  public ReportClick(){
    this.router.navigateByUrl('/Report');
  }
}
