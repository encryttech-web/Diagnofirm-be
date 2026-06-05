import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { RatingModule } from 'primeng/rating';
import { TextareaModule } from 'primeng/textarea';

@Component({
  selector: 'app-editfeedback',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgIf,
    NgFor,
    ButtonModule,
    DialogModule,
    InputTextModule,
    ToggleSwitchModule,
    RatingModule,
    TextareaModule
  ],
  templateUrl: './editfeedback.html',
  styleUrl: './editfeedback.scss'
})
export class Editfeedback {

  @Input() editdisplayfeedback: boolean = false;
  @Output() editdisplayChange: EventEmitter<any> = new EventEmitter<any>();

  @Input() feedbackData: any;

  feedbackid: number = 0;

  user_name: string = '';
  user_email: string = '';
  user_role: string = '';
  start_rating: string = '';
  fedbck_desc: string = '';
  is_active: boolean = true;

  getfeedbacklist: any;
  checked: boolean = true;
  userid: any;
  fedbck_ord: any;
  usr_id: any;

  constructor(
    private dataService: DataService,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private CDR: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.feedbackid = this.feedbackData?.id;
    this.getfeedbackbyId();
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.feedbackid = this.feedbackData?.id;
    this.getfeedbackbyId();
    this.CDR.detectChanges();
  }

  // CLOSE
  close() {
    this.editdisplayfeedback = false;
    this.editdisplayChange.emit(false);
  }

  // GET BY ID
  getfeedbackbyId() {

    const input = {
      feedbackid: Number(this.feedbackid)
    };

    const url = GlobalConstants.Authurl + GlobalConstants.GetfeedbackbyId;

    this.dataService.addData(url, input).subscribe((res: any) => {

      if (res.status === 'success') {

        this.getfeedbacklist = res.response.ref1[0];

        this.user_name = this.getfeedbacklist.username;
        this.user_email = this.getfeedbacklist.useremail;
        this.user_role = this.getfeedbacklist.userrole;
        this.start_rating = this.getfeedbacklist.starrating;
        this.fedbck_desc = this.getfeedbacklist.feedbackdesc;
        //this.is_active = this.getfeedbacklist.is_active === '1';
        this.checked = this.getfeedbacklist.is_active === '1';

        this.CDR.detectChanges();
      }
    });
  }

  // UPDATE
  editbtn(form: NgForm) {

    if (!form.valid) {
      this.notificationService.showMessage(
        'error',
        'Missing Fields',
        'Please fill all required fields'
      );
      return;
    }

    const input = {
      feedbackid: this.feedbackid,
      userid: this.usr_id,
      username: this.user_name,
      useremail: this.user_email,
      userrole: this.user_role,
      feedbackdesc: this.fedbck_desc,
      starrating: this.start_rating,
      feedbackord: this.fedbck_ord,
      createdby: this.userid,
      status: this.is_active ? '1' : '0'
    };

    const url = GlobalConstants.Authurl + GlobalConstants.Updatefeedback;

    this.dataService.addData(url, input).subscribe((res: any) => {

      if (res.status === 'success') {

        this.notificationService.showMessage(
          'success',
          'Updated',
          'Feedback updated successfully'
        );

        this.close();
      }
      else {
        this.notificationService.showMessage(
          'error',
          'Error',
          'Update failed'
        );
      }
    });
  }

  // CLEAR
  clear() {
    this.getfeedbackbyId();
  }
}