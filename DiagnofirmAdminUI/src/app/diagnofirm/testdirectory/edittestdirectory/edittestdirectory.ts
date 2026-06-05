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
  selector: 'app-edittestdirectory',
  imports: [
    TableModule,
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
    ToggleSwitchModule,
    NgIf,
    NgFor
  ],
  templateUrl: './edittestdirectory.html',
  styleUrl: './edittestdirectory.scss'
})
export class Edittestdirectory {

  @Input() editdisplaytestdirectory: boolean = false;
  @Output() editdisplayChange: EventEmitter<any> = new EventEmitter<any>();

  checked: boolean = true;
  gettestdirectorylist: any;
  testdirectoryid: any;

  // Add these properties
  industry: any = null;
  gettestdirectoryindustryist: any[] = [];

  @Input() testdirectorydata: any;

  testdirectoryhead: string = '';
  testdirectorycode: string = '';
  testdirectoryname: string = '';
  specimen: string = '';
  unit: string = '';
  refrange: string = '';
  testdescription: string = '';
  testorder: string = '';

  userdatavalue: any[] = [];
  userid: string = '';

  public userForm = new FormGroup({
    fctetdir_code: new FormControl("", [Validators.required]),
    fctetdir_name: new FormControl("", [Validators.required]),
    fctetdir_desc: new FormControl("", [Validators.required]),
  });

  constructor(
    private dataService: DataService,
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
  ) { }

  ngOnInit() {
    // const userInfo = window.sessionStorage.getItem('USERINFO');
    // this.userdatavalue = userInfo ? JSON.parse(userInfo) : null;

    // if (this.userdatavalue) {
    //   this.userid = this.userdatavalue[0].usercode;
    // }

    this.testdirectoryid = this.testdirectorydata['id'];
     this.gettestdirectoryindustry(); // ✅ Load industry list first
    this.gettestdirectorybyId();
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.testdirectoryid = this.testdirectorydata['id'];
     this.gettestdirectoryindustry(); // ✅ Load industry list first
    this.gettestdirectorybyId();
    this.CDR.detectChanges();
  }

  close() {
    this.editdisplaytestdirectory = false;
    this.editdisplayChange.emit(this.editdisplaytestdirectory);
  }

  // ✅ Add this method
  gettestdirectoryindustry() {
    const url = GlobalConstants.Authurl + GlobalConstants.GettestdirectoryIndustry;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status == 'success') {
        this.gettestdirectoryindustryist = response['response']['ref1'];
        this.CDR.detectChanges();
      }
    });
  }

  gettestdirectorybyId() {
    const input = { testdirectoryid: Number(this.testdirectoryid) };
    let url = GlobalConstants.Authurl + GlobalConstants.GettestdirectorybyId;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status == 'success') {
        this.editdisplaytestdirectory = true;
        this.gettestdirectorylist = response['response']['ref1'];

        this.testdirectoryhead = this.gettestdirectorylist[0].testdirectoryheadname;
        this.testdirectorycode = this.gettestdirectorylist[0].testdirectorycode;
        this.testdirectoryname = this.gettestdirectorylist[0].testdirectoryname;
        this.industry = this.gettestdirectorylist[0].industid;  // ✅ Bind industry
        this.specimen = this.gettestdirectorylist[0].specimen;
        this.unit = this.gettestdirectorylist[0].unit;
        this.refrange = this.gettestdirectorylist[0].referencerange;
        this.testdescription = this.gettestdirectorylist[0].testdirectorydescription;
        this.testorder = this.gettestdirectorylist[0].testorder;
        this.checked = this.gettestdirectorylist[0].is_active === '1';

        this.CDR.detectChanges();
      }
    });
  }

  editbtn(form: NgForm) {

    if (!form.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill in all required fields.');
      return;
    }

    // ✅ Validate industry
    if (!this.industry) {
      this.notificationService.showMessage('error', 'Validation', 'Please select an Industry.');
      return;
    }

    const input = {
      testdirectoryid: Number(this.testdirectoryid),
      industryid: this.industry,           // ✅ Added
      testdirectoryhead: this.testdirectoryhead,
      testdirectorycode: this.testdirectorycode,
      testdirectoryname: this.testdirectoryname,
      specimen: this.specimen,
      unit: this.unit,
      refrange: this.refrange,
      testdescription: this.testdescription,
      testorder: this.testorder,
      createdby: this.userid,
      status: this.checked ? '1' : '0',
    };

    let url = GlobalConstants.Authurl + GlobalConstants.Updatetestdirectory;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status == 'success') {
        this.close();
        this.editdisplayChange.emit(false);
        this.notificationService.showMessage('success', 'Updated', 'The test directory data has been successfully updated!');
        this.CDR.detectChanges();
      }
      else if (response.status == 'information') {
        this.notificationService.showMessage('error', 'Error', 'The data already exists.');
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'There was an issue updating the data.');
      }
    });
  }

  clear() {
    this.gettestdirectorybyId();
  }
}