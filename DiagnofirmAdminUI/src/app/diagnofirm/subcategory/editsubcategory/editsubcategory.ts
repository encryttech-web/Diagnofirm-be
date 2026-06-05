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
  selector: 'app-editsubcategory',
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
    ToggleSwitchModule, NgIf, NgFor, Imageupload,
    Imageview,
    EditorModule],
  templateUrl: './editsubcategory.html',
  styleUrl: './editsubcategory.scss'
})
export class Editsubcategory {

  @Input() editdisplaysubcategory: boolean = false;
  @Output() editdisplayChange: EventEmitter<any> = new EventEmitter<any>();
  public visibleImageView = false;
  images: any = [];

  checked: boolean = true;
  getsubcategoryist: any;
  subcategoryId: any;
  @Input() subcategorydata: any;
  subcategoryname: string = '';
  subcategorycode: string = '';
  username: string = '';
  subcategoryorder: string = '';
  subcategorydescription: string = '';
  userdatavalue: any[] = [];
  userid: string = '';

  //Image
  imageFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  imageFileval!: any[];
  testdata: any;
  visible!: boolean;
  @Input() rowvalue: any;
  deleteenable: boolean = true;
  imagejsonvalue: any;
  photoinfo: any;

  Imageinfo: any;
  imagename: any;

  categoryOptions: any;
  category: any;

  public userForm = new FormGroup({
    fcsubcategorycode: new FormControl("", [Validators.required]),
    fcsubcategoryname: new FormControl("", [Validators.required]),
    fcsubcategoryorder: new FormControl("", [Validators.required]),
    fcsubcategorydescription: new FormControl("", [Validators.required]),
  });
  getcategoryist: any;

  constructor(
    private dataService: DataService, private HTTPSERVICE: HttpService, private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService, private notificationService: NotificationService, private COMPRESSIMAGESERVICE: CompressImageService,
    private sanitizer: DomSanitizer
  ) { }


  ngOnInit() {
    this.testdata = true;
    this.getcategory();
    //Userinfo
    // const userInfo = window.sessionStorage.getItem('USERINFO');
    // this.userdatavalue = userInfo ? JSON.parse(userInfo) : null;
    // if (this.userdatavalue) {
    //   this.userid = this.userdatavalue[0].usercode;
    // }

    if (!this.subcategorydata) return;

    this.subcategoryId = this.subcategorydata?.id;
    this.Imageinfo = this.subcategorydata['subcategoryimage'];
    this.imagename = this.subcategorydata['subcategoryimagename'];
    if (this.subcategoryId) {
      this.getsubcategorybyId();
    }

    this.CDR.detectChanges();
  }

  ngOnChanges() {
    // this.Imageinfo = this.subcategorydata['subcategoryimage'];
    // this.imagename = this.subcategorydata['subcategoryimagename'];
    this.subcategoryId = this.subcategorydata?.id;
    this.getsubcategorybyId();
    this.CDR.detectChanges();
  }

  ngAfterViewInit() {
  }

  close() {
    this.editdisplaysubcategory = false;
    this.editdisplayChange.emit(this.editdisplaysubcategory);
  }

  deleteenablebtn() {
    this.Imageinfo = null;
    this.deleteenable = false;
  }

  onImageRemove() {
    this.images = null;
    this.imagename = null;
    this.deleteenable = false; // show upload
  }



  getcategory() {

    //let url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.Getcategory;
    let url = GlobalConstants.Authurl + GlobalConstants.Getcategory;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status == 'success') {
        this.getcategoryist = response['response']['ref1'];
        this.CDR.detectChanges();
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'There is no data .');
      }
    });

  }


  getsubcategorybyId() {

    const input = {
      subcategoryid: Number(this.subcategoryId),
    }

    //let url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.GetsubcategorybyId;
    let url = GlobalConstants.Authurl + GlobalConstants.GetsubcategorybyId;

    this.dataService.addData(url, input).subscribe((response: any) => {
      console.log(response);
      if (response.status == 'success') {
        this.editdisplaysubcategory = true;
        this.getsubcategoryist = response['response']['ref1'];
        this.category = this.getsubcategoryist[0].categoryid;
        this.subcategorycode = this.getsubcategoryist[0].subcategorycode;
        this.subcategoryname = this.getsubcategoryist[0].subcategoryname;
        this.subcategoryorder = this.getsubcategoryist[0].subcategoryorder;
        this.subcategorydescription = this.getsubcategoryist[0].subcategorydescription;
        this.checked = this.getsubcategoryist[0].is_active === '1';
        this.deleteenable = true;

        // this.images = {
        //   imagenamevalue: this.getsubcategoryist[0].subcategoryimagename,
        //   imageBase64value: 'data:image/jpeg;base64,' + this.getsubcategoryist[0].subcategoryimage
        // };
        this.images = {
          imagenamevalue: this.getsubcategoryist[0].subcategoryimagename,
          imageBase64value: this.sanitizer.bypassSecurityTrustUrl(
            'data:image/jpeg;base64,' + this.getsubcategoryist[0].subcategoryimage
          )
        };
        this.CDR.detectChanges();
      }
      else {
        return;
      }
    });

  }

  getImagemasterbyId(rowid: any) {

    const input = {
      subcatid: rowid,
      username: '1'
    };

    // let url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.GetBySubcatIdviewImage;
    let url = GlobalConstants.Authurl + GlobalConstants.GetBySubcatIdviewImage;

    this.HTTPSERVICE.post(url, input).subscribe((response: any) => {

      if (response.status == 'success') {
        this.images = [];
        const img = response['response'][0];

        //  this.images = {
        //   imagenamevalue: img.imagename,
        //   imageBase64value: 'data:image/jpeg;base64,' + img.imagedata
        // };

        this.visibleImageView = true;  // Show the image component
        this.visible = false;

        this.images = {
          imagenamevalue: img.imagename,
          imageBase64value: this.sanitizer.bypassSecurityTrustUrl(
            // 'data:image/jpeg;base64,' + img.imagedata
            img.imageBase64value
          )
        };

        //this.imageBase64value = this.images.imagedata;

        this.visibleImageView = true;
        this.CDR.detectChanges();
      }
      else {
        // this.visible = !this.visible;
        // this.messagebakclr = 'danger';
        return;
      }
    });

  }

  // =========================
  // IMAGE
  // =========================
  onImageSelect(event: any) {
    if (event.files && event.files.length > 0) {
      this.imageFile = event.files[0];

      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result;
      };
      if (this.imageFile) {
        reader.readAsDataURL(this.imageFile);
      }
    }
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

  imageviwe() {
    //this.visible = !this.visible;
    this.visibleImageView = false;  // Show the image component
    //this.visible = false;
    this.images = null;

    this.getImagemasterbyId(this.subcategoryId);
  }


  editbtn(subcategoryForm: NgForm) {

    if (!subcategoryForm.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill in all required fields.');
      return;
    }

    const formData = new FormData();

    formData.append('subcategoryid', String(this.subcategoryId));
    formData.append('categoryid', String(this.category));
    formData.append('subcategorycode', this.subcategorycode);
    formData.append('subcategoryname', this.subcategoryname);
    formData.append('subcategorydescription', this.subcategorydescription);
    formData.append('subcategoryorder', this.subcategoryorder);

    if (this.deleteenable === true) {
      this.imagejsonvalue = this.Imageinfo;
      this.photoinfo = this.imagename;
    }
    else {
      this.imagejsonvalue = this.imageFileval[0];
      this.photoinfo = this.imageFileval[0].name;
    }

    formData.append("subcategoryimage", this.imagejsonvalue);
    formData.append("subcategoryimagename", this.photoinfo);

    formData.append('createdby', this.userid);
    formData.append('status', this.checked ? '1' : '0');

    //let url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.Updatesubcategory;
    let url = GlobalConstants.Authurl + GlobalConstants.Updatesubcategory;

    this.dataService.addData(url, formData).subscribe((response: any) => {
      if (response.status == 'success') {

        this.close();
        this.editdisplayChange.emit(false);
        this.CDR.detectChanges();
        this.notificationService.showMessage('success', 'subcategory Updated', 'The subcategory data has been successfully updated!');
      }
      else if (response.status == 'information') {
        this.notificationService.showMessage('error', 'Error', 'The data already exists. Please try again with different information.');
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'There was an issue updating the subcategory data.');
      }
    });
    // }
  }

  clear() {
    this.getsubcategorybyId();
    // this.getsubcategoryist = null;
    // this.checked = false;
  }

}
