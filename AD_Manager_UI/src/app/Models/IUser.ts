import { getLocaleDateFormat } from "@angular/common";

export class IUser {
    firstName: string = "";
    grade    :     string = "";
    halat_eshteghal    :     string = "";
    jobStringName    :     string = "";
    lastName    :     string = "";
    major    :     string = "";
    nationalCode    :     string = "";
    password    :     string = "";
    personCode    :     string = "";
    personNo    :     string = "";
    shaghel    :     boolean = false;
    unit_khedmat    :     string = "";
    UserName    :     string = "";
    workplace    :     string = "";
    ouplace    :     string = "LDAP://OU=test-rabbani,OU=e-users,OU=Employee,OU=ITC-MUMS,OU=Data Center,OU=BranchOffice,DC=mums,DC=ac,DC=ir";
    email : string = "";
    isactive: boolean = false;

  }

export class IUserToken {

    constructor(public userName: string, public token: string, private tokenExpirationDate: Date ){
    }
}
  