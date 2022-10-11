import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ManagmentService } from '../managment.service';
import { IUser } from '../Models/IUser';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  loading: boolean = false;
  headers = ["ID", "Name", "LastName", "NationalCode", "Grade", "Workplace"];
  rows: IUser[] = [];
  TestRow: any[] = [];
  TestRowUser: IUser[] = [];
  constructor(private service: ManagmentService, private router: Router, private changeDetector : ChangeDetectorRef) { }

  ngOnInit(): void {
    this.service.autoLogin();
    if(!ManagmentService.isAuthentication){
      this.router.navigate(['/Login', 'UserList']);
      return;
    }
    //console.log('oninit');
    this.loadCustomers();
  }

  ngAfterContentChecked() : void {
    this.changeDetector.detectChanges();
  }

  loadCustomers(){
    this.loading = true;
    //console.log('LazyLoad');
    this.service.getUserList()
      .subscribe(
        (response) => { 
        //console.log('Loading response');

          this.rows=response;
          this.loading = false;
          //console.log('Loaded response');
      },
        error => {        
          this.loading = false;
          console.log(error);
          
        }
      );
  }

  public RegisterClick(event: any, item: IUser) {
    this.router.navigate(['/register-new-user', item.personCode]);
  }

  public SearchInTable(value : string): string {
    //dt2.filterGlobal($any($event).target.value
    return value.replace(String.fromCharCode(1603), String.fromCharCode(1705))
      .replace(String.fromCharCode(1610), String.fromCharCode(1740));
  }
}
