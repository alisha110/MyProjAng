import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatSliderModule } from '@angular/material/slider';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserListComponent } from './user-list/user-list.component';
//import { RegisterNewUserComponent } from './register-new-user/register-new-user.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
//import { SetActivatioUserComponent } from './set-activatio-user/set-activatio-user.component';
import { ModalModule } from 'ngx-bootstrap/modal';
//import { ReportComponent } from './report/report.component';

@NgModule({
  declarations: [
    AppComponent,
    UserListComponent,
    //RegisterNewUserComponent,
    LoginComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    //SetActivatioUserComponent,
    //ReportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatSliderModule,
    BrowserAnimationsModule,
    TableModule,
    ButtonModule,
    ModalModule.forRoot()
  ],
  exports: [ TableModule ] ,
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
