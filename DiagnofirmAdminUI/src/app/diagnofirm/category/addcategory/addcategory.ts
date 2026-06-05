import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { FloatLabelModule } from 'primeng/floatlabel';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { SelectModule } from 'primeng/select';
import { SliderModule } from 'primeng/slider';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ToggleSwitchModule } from 'primeng/toggleswitch';

@Component({
  selector: 'app-addcategory',
  imports: [TableModule,
    MultiSelectModule,
    SelectModule,
    InputIconModule,
    TagModule,
    InputTextModule,
    SliderModule,
    ProgressBarModule,
    ToggleButtonModule,
    ToastModule,
    CommonModule,
    FormsModule,
    ButtonModule,
    RatingModule,
    RippleModule,
    IconFieldModule,
    DialogModule,
    FloatLabelModule,
    DatePickerModule,
    ToggleSwitchModule],
  templateUrl: './addcategory.html',
  styleUrl: './addcategory.scss'
})
export class Addcategory {

  @Input() display: boolean = false;  // Accept the display property from the parent
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() dataReloaded: EventEmitter<any> = new EventEmitter();
  checked: boolean = true;
  calendarValue: any = null;
  departmentitem: any = null;
  roleitem: any = null;
  departmentlist: any;
  rolelist: any;
  userdatavalue: any[] = [];
  userid: string = '';

  categoryname: string = '';
  categorycode: string = '';
  username: string = '';
  categoryorder: string = '';
  categorydescription: string = '';

  public userForm = new FormGroup({
    fccategoryname: new FormControl("", [Validators.required]),
    fccategorycode: new FormControl("", [Validators.required]),
    fcstatus: new FormControl("1", [Validators.required]),
  });
  Lastcode: any;
  Lastcodevalue: any;

  constructor(
    private dataService: DataService, private HTTPSERVICE: HttpService, private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService, private notificationService: NotificationService,
  ) { }


  ngOnInit() {
    // const userInfo = window.sessionStorage.getItem('USERINFO');
    // this.userdatavalue = userInfo ? JSON.parse(userInfo) : null;
    // if (this.userdatavalue) {
    //   this.userid = this.userdatavalue[0].usercode;
    // }

    this.getlastcode('diafrm', 'category_tbl', 'cat_code');
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.getlastcode('diafrm', 'category_tbl', 'cat_code');
    this.CDR.detectChanges();
  }

  ngAfterViewInit() {
  }

  close() {
    this.display = false;
    this.displayChange.emit(this.display);
    this.dataReloaded.emit();
  }

  generateNextCode(lastCode: string): string {
    const prefix = 'CAT';

    if (!lastCode) {
      return `${prefix}-0001`;
    }

    // Step 1: Extract numeric part
    const lastNumber = parseInt(lastCode.split('-')[1], 10);

    // Step 2: Increment
    const nextNumber = lastNumber + 1;

    // Step 3: Format with leading zeros (4 digits)
    const formatted = nextNumber.toString().padStart(4, '0');

    return `${prefix}-${formatted}`;
  }


  getlastcode(schemaname: any, tablename: any, columnname: any) {

    const input = {
      schemaname: schemaname,
      tablename: tablename,
      columnname: columnname
    }

    let url = GlobalConstants.Authurl + GlobalConstants.Getlastcode;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status == 'success') {

        const value = response?.response?.ref1?.[0]?.ref1 ?? 'CAT-0000';
        this.Lastcode = value;
        this.categorycode = this.generateNextCode(this.Lastcode);

      }
    });
  }


  addbtn(categoryForm: NgForm) {

    if (!categoryForm.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill in all required fields.');
      return;
    }

    const input = {
      categorycode: this.categorycode,
      categoryname: this.categoryname,
      categoryorder: this.categoryorder,
      categorydescription: this.categorydescription,
      createdby: this.userid,
      status: this.checked ? '1' : '0',
    };

    //let url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.Addcategory;

    let url = GlobalConstants.Authurl + GlobalConstants.Addcategory;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status == 'success') {

        this.close();
        this.notificationService.showMessage('success', 'category Added', 'The category data has been successfully added!');
      }
      else if (response.status == 'information') {
        this.notificationService.showMessage('error', 'Error', 'The data already exists. Please try again with different information.');
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'There was an issue adding the category data.');
      }
    });
  }

  clear() {

    this.categoryname = '';
    this.categorycode = '';
    this.username = '';
    this.categoryorder = '';
    this.categorydescription = '';
    this.checked = false;
    this.calendarValue = null;

  }

}
