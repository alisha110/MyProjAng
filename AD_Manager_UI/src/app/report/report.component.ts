import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ManagmentService } from '../managment.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {

  constructor(private service: ManagmentService, private router: Router, private changeDetector : ChangeDetectorRef) { }
  loading: boolean = false;
  rows: any;

  ngOnInit(): void {
    this.service.autoLogin();
    if(!ManagmentService.isAuthentication){
      this.router.navigate(['/Login', '']);
      return;
    }
    this.loadReprots();
  }

  ngAfterContentChecked() : void {
    this.changeDetector.detectChanges();
  }

  loadReprots(){
    this.loading = true;
    this.service.getReportList()
      .subscribe(
        (response) => { 
          console.log(response);
          
          this.rows=response;
          this.loading = false;
      },
        error => {        
          this.loading = false;
          console.log(error);
          
          //this.service.ErrorManagment(error);
        }
      );
  }

  public SearchInTable(value : string): string {
    //dt2.filterGlobal($any($event).target.value
    return value.replace(String.fromCharCode(1603), String.fromCharCode(1705))
      .replace(String.fromCharCode(1610), String.fromCharCode(1740));
  }
}
