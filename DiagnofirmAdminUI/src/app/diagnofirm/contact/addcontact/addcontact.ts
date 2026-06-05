import { Imageupload } from '@/diagnofirm/common/imageupload/imageupload';
import { CompressImageService } from '@/diagnofirm/services/compress-image.service';
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
import { FileUploadModule } from 'primeng/fileupload';
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
  selector: 'app-addcontact',
  standalone: true,
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
    FileUploadModule,
    Imageupload,
  ],
  templateUrl: './addcontact.html',
  styleUrl: './addcontact.scss',
})
export class Addcontact {

  @Input() display: boolean = false;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() dataReloaded: EventEmitter<any> = new EventEmitter();

  checked: boolean = false;
  userdatavalue: any[] = [];
  userid: string = '';

  // Fields mapped to diafrm.contact_tbl columns
  conttyp_name: string ='';
  conttype: any;           // cont_type
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

  imageFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  imageFileval!: any[];
  imagejsonvalue: any;
  photoinfo: any;

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
  ) {}

  ngOnInit() {
    this.getcontacttype();
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.getcontacttype();
    this.CDR.detectChanges();
  }

  ngAfterViewInit() {}

  getcontacttype() {
    const url = GlobalConstants.Authurl + GlobalConstants.Getcontacttype;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status === 'success') {
        this.contacttypelist = response['response']['ref1'];
        this.CDR.detectChanges();
      } else {
        this.notificationService.showMessage('error', 'Error', 'Unable to load contact types.');
      }
    });
  }

  async someMethod(event: any) {
    this.imageFileval = await this.gettingFile(event);
  }

  async gettingFile(imagefile: any) {
    const conImgFileArray: any[] = [];
    if (!imagefile) return [];
    const compressed = await this.compressFile(imagefile);
    conImgFileArray.push(compressed);
    return conImgFileArray;
  }

  async compressFile(file: any) {
    const result = await firstValueFrom(
      this.COMPRESSIMAGESERVICE.compress(file).pipe(take(1))
    );
    return result;
  }

  close() {
    this.display = false;
    this.displayChange.emit(this.display);
    this.dataReloaded.emit();
  }

  addbtn(contactForm: NgForm) {
    if (!contactForm.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill in all required fields.');
      return;
    }

    // const formData = new FormData();
    // formData.append('conttype', this.conttype);
    // formData.append('contname', this.contname);
    // formData.append('contaddress', this.contaddress);
    // formData.append('contcity', this.contcity);
    // formData.append('contstate', this.contstate);
    // formData.append('contcountry', this.contcountry);
    // formData.append('contphno', this.contphno);
    // formData.append('contaltphno', this.contaltphno);
    // formData.append('contwrkhrs1', this.contwrkhrs1);
    // formData.append('contwrkhrs2', this.contwrkhrs2);
    // formData.append('contwrkhrs3', this.contwrkhrs3);
    // formData.append('contemail', this.contemail);
    // formData.append('contdircts', this.contdircts);
    // formData.append('contdesc', this.contdesc);
    // formData.append('contord', this.contord);
    // formData.append('createdby', this.userid);
    // formData.append('status', this.checked ? '1' : '0');

    const input = {

      conttype: this.conttype?.id ?? this.conttype,
      contname : this.contname,
      contaddress: this.contaddress,
      contcity : this.contcity,
      contstate : this.contstate,
      contcountry : this.contcountry,
      contphno : this.contphno,
      contaltphno : this.contaltphno,
      contwrkhrs1 : this.contwrkhrs1,
      contwrkhrs2 : this.contwrkhrs2,
      contwrkhrs3 : this.contwrkhrs3,
      contemail : this.contemail,
      contdircts : this.contdircts,
      contdesc: this.contdesc,
      contord : this.contord,
      createdby : this.userid,
      status : this.checked ? '1' : '0'
    }

    const url = GlobalConstants.Authurl + GlobalConstants.Addcontact;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status === 'success') {
        this.close();
        this.notificationService.showMessage('success', 'Contact Added', 'The contact has been successfully added!');
      } else if (response.status === 'information') {
        this.notificationService.showMessage('error', 'Error', 'This contact already exists. Please try with different information.');
      } else {
        this.notificationService.showMessage('error', 'Error', 'There was an issue adding the contact.');
      }
    });
  }

  clear() {
    this.conttyp_name = '';
    this.conttype = null;
    this.contname = '';
    this.contaddress = '';
    this.contcity = '';
    this.contstate = '';
    this.contcountry = '';
    this.contphno = '';
    this.contaltphno = '';
    this.contwrkhrs1 = '';
    this.contwrkhrs2 = '';
    this.contwrkhrs3 = '';
    this.contemail = '';
    this.contdircts = '';
    this.contdesc = '';
    this.contord = '';
    this.checked = false;
  }
}