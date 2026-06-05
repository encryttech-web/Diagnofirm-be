import { Imageupload } from '@/diagnofirm/common/imageupload/imageupload';
import { Imageview } from '@/diagnofirm/common/imageview/imageview';
import { CompressImageService } from '@/diagnofirm/services/compress-image.service';
import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { EditorModule } from 'primeng/editor';
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
import { firstValueFrom, take } from 'rxjs';

@Component({
  selector: 'app-editcontact',
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
    NgFor,
    Imageupload,
    Imageview,
    EditorModule,
  ],
  templateUrl: './editcontact.html',
  styleUrl: './editcontact.scss',
})
export class Editcontact {

  @Input() editdisplaycontact: boolean = false;
  @Output() editdisplayChange: EventEmitter<any> = new EventEmitter<any>();

  public visibleImageView = false;
  images: any = [];

  checked: boolean = false;
  contactId: any;

  @Input() contactdata: any;

  // Fields mapped to diafrm.contact_tbl columns
  conttyp_name: string = '';
  conttype: any = null;           // cont_type
  contname: string = '';           // cont_name
  contaddress: string = '';        // cont_address
  contcity: string = '';           // cont_city
  contstate: string = '';          // cont_state
  contcountry: string = '';        // cont_country
  contphno: string = '';           // cont_phno
  contaltphno: string = '';        // cont_altphno
  contwrkhrs1: string = '';        // cont_wrkhrs1
  contwrkhrs2: string = '';        // cont_wrkhrs2
  contwrkhrs3: string = '';        // cont_wrkhrs3
  contemail: string = '';          // cont_email
  contdircts: string = '';         // cont_dircts
  contdesc: string = '';           // cont_desc
  contord: string = '';            // cont_ord

  contacttypelist: any[] = [];
  getcontactlist: any;

  userdatavalue: any[] = [];
  userid: string = '';

  // Image
  imageFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  imageFileval!: any[];
  visible!: boolean;

  @Input() rowvalue: any;
  deleteenable: boolean = true;
  imagejsonvalue: any;
  photoinfo: any;
  Imageinfo: any;
  imagename: any;

  public contactFormGroup = new FormGroup({
    fccontname: new FormControl('', [Validators.required]),
    fccontemail: new FormControl('', [Validators.required, Validators.email]),
    fccontphno: new FormControl('', [Validators.required]),
    fcconttype: new FormControl('', [Validators.required]),
  });

  constructor(
    private dataService: DataService,
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private COMPRESSIMAGESERVICE: CompressImageService,
    private sanitizer: DomSanitizer,
  ) { }

  ngOnInit() {
    this.getcontacttype();

    if (!this.contactdata) return;

    this.contactId = this.contactdata?.id;

    if (this.contactId) {
      this.getcontactbyId();
    }

    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.contactId = this.contactdata?.id;
    this.getcontactbyId();
    this.CDR.detectChanges();
  }

  ngAfterViewInit() { }

  close() {
    this.editdisplaycontact = false;
    this.editdisplayChange.emit(this.editdisplaycontact);
  }

  deleteenablebtn() {
    this.Imageinfo = null;
    this.deleteenable = false;
  }

  onImageRemove() {
    this.images = null;
    this.imagename = null;
    this.deleteenable = false;
  }

  getcontacttype() {
    const url = GlobalConstants.Authurl + GlobalConstants.Getcontacttype;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status === 'success') {
        this.contacttypelist = response['response']['ref1'];
        if (this.getcontactlist?.length) {
          this.conttype = this.contacttypelist.find(
            item => item.id == this.getcontactlist[0].cont_type
          ) ?? null;
        }
        this.CDR.detectChanges();
      } else {
        this.notificationService.showMessage('error', 'Error', 'Unable to load contact types.');
      }
    });
  }

  getcontactbyId() {
    const input = {
      contactid: Number(this.contactId),
    };

    const url = GlobalConstants.Authurl + GlobalConstants.Getcontactbyid;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status === 'success') {
        this.editdisplaycontact = true;
        this.getcontactlist = response['response']['ref1'];

        // Map response fields to SQL column names
        const contact = this.getcontactlist[0];
        // this.conttype    = contact.cont_type;
        this.conttype = this.contacttypelist.find(
          item => item.id == contact.cont_type
        ) ?? null;
        this.contname = contact.cont_name;
        this.contaddress = contact.cont_address;
        this.contcity = contact.cont_city;
        this.contstate = contact.cont_state;
        this.contcountry = contact.cont_country;
        this.contphno = contact.cont_phno;
        this.contaltphno = contact.cont_altphno;
        this.contwrkhrs1 = contact.cont_wrkhrs1;
        this.contwrkhrs2 = contact.cont_wrkhrs2;
        this.contwrkhrs3 = contact.cont_wrkhrs3;
        this.contemail = contact.cont_email;
        this.contdircts = contact.cont_dircts;
        this.contdesc = contact.cont_desc;
        this.contord = contact.cont_ord;
        this.checked = contact.is_active === '1';

        this.CDR.detectChanges();
      } else {
        return;
      }
    });
  }

  editbtn(contactForm: NgForm) {
    if (!contactForm.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill in all required fields.');
      return;
    }

    // const formData = new FormData();
    // formData.append('contactid',  String(this.contactId));
    // formData.append('conttype',   this.conttype);
    // formData.append('contname',   this.contname);
    // formData.append('contaddress', this.contaddress);
    // formData.append('contcity',   this.contcity);
    // formData.append('contstate',  this.contstate);
    // formData.append('contcountry', this.contcountry);
    // formData.append('contphno',   this.contphno);
    // formData.append('contaltphno', this.contaltphno);
    // formData.append('contwrkhrs1', this.contwrkhrs1);
    // formData.append('contwrkhrs2', this.contwrkhrs2);
    // formData.append('contwrkhrs3', this.contwrkhrs3);
    // formData.append('contemail',  this.contemail);
    // formData.append('contdircts', this.contdircts);
    // formData.append('contdesc',   this.contdesc);
    // formData.append('contord',    this.contord);
    // formData.append('createdby',  this.userid);
    // formData.append('status',     this.checked ? '1' : '0');

    const input = {
      contactid: String(this.contactId),
      conttype: this.conttype?.id ?? this.conttype,
      contname: this.contname,
      contaddress: this.contaddress,
      contcity: this.contcity,
      contstate: this.contstate,
      contcountry: this.contcountry,
      contphno: this.contphno,
      contaltphno: this.contaltphno,
      contwrkhrs1: this.contwrkhrs1,
      contwrkhrs2: this.contwrkhrs2,
      contwrkhrs3: this.contwrkhrs3,
      contemail: this.contemail,
      contdircts: this.contdircts,
      contdesc: this.contdesc,
      contord: this.contord,
      createdby: this.userid,
      status: this.checked ? '1' : '0'
    }

    const url = GlobalConstants.Authurl + GlobalConstants.Updatecontact;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status === 'success') {
        this.close();
        this.editdisplayChange.emit(false);
        this.CDR.detectChanges();
        this.notificationService.showMessage('success', 'Contact Updated', 'The contact has been successfully updated!');
      } else if (response.status === 'information') {
        this.notificationService.showMessage('error', 'Error', 'This contact already exists. Please try with different information.');
      } else {
        this.notificationService.showMessage('error', 'Error', 'There was an issue updating the contact.');
      }
    });
  }

  clear() {
    this.getcontactbyId();
  }
}