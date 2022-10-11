import { getLocaleDateFormat, Time } from "@angular/common";

export class IReport {
    // public long ID { get; set; }
    id: number = 0;
    // public int? Level { get; set; }
    level: number = 0;
    // public string? Message { get; set; }
    message: string = "";
    // public bool Result { get; set; }
    result: boolean = false;
    // public string? ErrorMessage { get; set; }
    errorMessage: string= "";
    // public string? UserName { get; set; }
    userName: string = "";
    // public DateTime? RegisterDate { get; set; }
    registerDate: Date = new Date();
}