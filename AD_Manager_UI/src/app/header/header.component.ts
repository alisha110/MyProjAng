import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ManagmentService } from '../managment.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  constructor(private service: ManagmentService, private router: Router ) { }

  ngOnInit(): void {
  }

  get staticUrlArray() {
    return ManagmentService.isAuthentication;
  }

  public logout(){
    this.service.logout();
    this.router.navigateByUrl('/Login');
  }

}
