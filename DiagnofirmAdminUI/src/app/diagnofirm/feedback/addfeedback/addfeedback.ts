import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';

import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, FormGroup, FormControl, Validators, NgForm } from '@angular/forms';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { RatingModule } from 'primeng/rating';

@Component({
  selector: 'app-addfeedback',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    SelectModule,
    ToggleSwitchModule,
    RatingModule
  ],
  templateUrl: './addfeedback.html',
  styleUrl: './addfeedback.scss'
})
export class Addfeedback {

  // ================= INPUT / OUTPUT =================
  @Input() display: boolean = false;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() dataReloaded = new EventEmitter<any>();

  // ================= FEEDBACK FIELDS =================
  usr_id: string = '';
  user_name: string = '';
  user_email: string = '';
  user_role: string = '';

  fedbck_desc: string = '';
  fedbck_ord: string = '';

  start_rating: number = 0;

  checked: boolean = true;

  userid: string = '';

  // ================= FORM =================
  public feedbackForm = new FormGroup({
    fcusername: new FormControl('', [Validators.required]),
    fcemail: new FormControl('', [Validators.required]),
    fcrating: new FormControl('', [Validators.required]),
    fcstatus: new FormControl('1', [Validators.required])
  });
  feedback_code: string = '';
  Lastcode: any;

  constructor(
    private dataService: DataService,
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService
  ) {}

  // ================= INIT =================
  ngOnInit() {
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.CDR.detectChanges();
  }


  // ================= CLOSE =================
  close() {
    this.display = false;
    this.displayChange.emit(this.display);
    this.dataReloaded.emit();
  }

  // ================= ADD FEEDBACK =================
  addbtn(form: NgForm) {

    if (!form.valid) {
      this.notificationService.showMessage(
        'error',
        'Missing Fields',
        'Please fill in all required fields.'
      );
      return;
    }

    const input = {
      userid: this.usr_id,
      username: this.user_name,
      useremail: this.user_email,
      userrole: this.user_role,
      feedbackdesc: this.fedbck_desc,
      starrating: this.start_rating,
      feedbackord: this.fedbck_ord,
      createdby: this.userid,
      status: this.checked ? '1' : '0'
    };

    let url = GlobalConstants.Authurl + GlobalConstants.Addfeedback;

    this.dataService.addData(url, input).subscribe((response: any) => {

      if (response.status == 'success') {

        this.close();

        this.notificationService.showMessage(
          'success',
          'Feedback Added',
          'Feedback added successfully!'
        );

      } else if (response.status == 'information') {

        this.notificationService.showMessage(
          'error',
          'Error',
          'Feedback already exists'
        );

      } else {

        this.notificationService.showMessage(
          'error',
          'Error',
          'Failed to add feedback'
        );

      }

    });
  }

  // ================= CLEAR =================
  clear() {

    this.usr_id = '';
    this.user_name = '';
    this.user_email = '';
    this.user_role = '';

    this.fedbck_desc = '';
    this.fedbck_ord = '';

    this.start_rating = 0;
    this.checked = true;
  }
}