import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule, NgIf, NgFor } from '@angular/common';
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
  selector: 'app-editcategory',
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
    ToggleSwitchModule, NgIf, NgFor],
  templateUrl: './editcategory.html',
  styleUrl: './editcategory.scss'
})
export class Editcategory {

   @Input() editdisplaycategory: boolean = false;
  @Output() editdisplayChange: EventEmitter<any> = new EventEmitter<any>();
  checked: boolean = true;
  getcategoryist: any;
  categoryId: any;
  @Input() categorydata: any;
  categoryname: string = '';
  categorycode: string = '';
  username: string = '';
  categoryorder: string = '';
  categorydescription: string = '';
  userdatavalue: any[] = [];
  userid: string = '';

  public userForm = new FormGroup({
    fccategorycode: new FormControl("", [Validators.required]),
    fccategoryname: new FormControl("", [Validators.required]),
    fclocationnname: new FormControl("", [Validators.required]),
    fccategorydescription: new FormControl("", [Validators.required]),
  });

  constructor(
    private dataService: DataService, private HTTPSERVICE: HttpService, private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService, private notificationService: NotificationService,
  ) { }


  ngOnInit() {
    //Userinfo
    // const userInfo = window.sessionStorage.getItem('USERINFO');
    // this.userdatavalue = userInfo ? JSON.parse(userInfo) : null;
    // if (this.userdatavalue) {
    //   this.userid = this.userdatavalue[0].usercode;
    // }
    this.categoryId = this.categorydata['id'];
    this.getcategorybyId();
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.categoryId = this.categorydata['id'];
    this.getcategorybyId();
    this.CDR.detectChanges();
  }

  ngAfterViewInit() {
  }

  close() {
    this.editdisplaycategory = false;
    this.editdisplayChange.emit(this.editdisplaycategory);
  }

  getcategorybyId() {

    const input = {
      // username: this.userid,
      categoryid: Number(this.categoryId),
    }

    //let url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.GetcategorybyId;
    let url = GlobalConstants.Authurl + GlobalConstants.GetcategorybyId;

    this.dataService.addData(url, input).subscribe(
      (response: any) => {
        console.log(response);
        if (response.status == 'success') {
          this.editdisplaycategory = true;
          this.getcategoryist = response['response']['ref1'];
          this.categorycode = this.getcategoryist[0].categorycode;
          this.categoryname = this.getcategoryist[0].categoryname;
          this.categoryorder = this.getcategoryist[0].categoryorder;
          this.categorydescription = this.getcategoryist[0].categorydescription;
          this.checked = this.getcategoryist[0].is_active === '1';
          this.CDR.detectChanges();
        }
        else {
          return;
        }
      });

  }

  editbtn(categoryForm: NgForm) {

    if (!categoryForm.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill in all required fields.');
      return;
    }

    // if (this.getcategoryist && this.getcategoryist[0]) {
    const input = {
      categoryid: Number(this.categoryId),
      categorycode: this.categorycode,
      categoryname: this.categoryname,
      categoryorder: this.categoryorder,
      categorydescription: this.categorydescription,
      createdby: this.userid,
      status: this.checked ? '1' : '0'
    };

    //let url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.Updatecategory;

    let url = GlobalConstants.Authurl + GlobalConstants.Updatecategory;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status == 'success') {

        this.close();
        this.editdisplayChange.emit(false);
        this.CDR.detectChanges();
        this.notificationService.showMessage('success', 'category Updated', 'The category data has been successfully updated!');
      }
      else if (response.status == 'information') {
        this.notificationService.showMessage('error', 'Error', 'The data already exists. Please try again with different information.');
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'There was an issue updating the category data.');
      }
    });
    // }
  }

  clear() {
    this.getcategorybyId();
    // this.getcategoryist = null;
    // this.checked = false;
  }

}
