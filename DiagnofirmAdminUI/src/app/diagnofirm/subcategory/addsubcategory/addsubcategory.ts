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
  selector: 'app-addsubcategory',
  standalone: true,
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
    ToggleSwitchModule,
    FileUploadModule,
  Imageupload],
  templateUrl: './addsubcategory.html',
  styleUrl: './addsubcategory.scss'
})
export class Addsubcategory {

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

  subcategoryname: string = '';
  subcategorycode: string = '';
  username: string = '';
  subcategoryorder: string = '';
  subcategorydescription: string = '';

  categoryOptions: any;
  category: number | null = null;

  imageFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;

  imageFileval!: any[];
  imagejsonvalue: any;
  testdata: any;

  public userForm = new FormGroup({
    fcsubcategoryname: new FormControl("", [Validators.required]),
    fcsubcategorycode: new FormControl("", [Validators.required]),
    fcstatus: new FormControl("1", [Validators.required]),
  });
  Lastcode: any;
  getcategoryist: any;
  photoinfo: any;

  constructor(
    private dataService: DataService, private HTTPSERVICE: HttpService, private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService, private notificationService: NotificationService, private COMPRESSIMAGESERVICE: CompressImageService,
  ) { }


  ngOnInit() {
    // const userInfo = window.sessionStorage.getItem('USERINFO');
    // this.userdatavalue = userInfo ? JSON.parse(userInfo) : null;
    // if (this.userdatavalue) {
    //   this.userid = this.userdatavalue[0].usercode;
    // }

    this.getcategory();
    this.getlastcode('diafrm', 'subcategory_tbl', 'subcat_code');
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.getcategory();
    this.getlastcode('diafrm', 'subcategory_tbl', 'subcat_code');
    this.CDR.detectChanges();
  }

  ngAfterViewInit() {
  }

  generateNextCode(lastCode: string): string {
    const prefix = 'SUBCAT';

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

        const value = response?.response?.ref1?.[0]?.ref1 ?? 'SUBCAT-0000';
        this.Lastcode = value;
        this.subcategorycode = this.generateNextCode(this.Lastcode);

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

  onImageSelect(event: any) {
    if (event.files && event.files.length > 0) {
      this.imageFile = event.files[0];

      // preview
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result;
      };
      if (!this.imageFile) {
        this.notificationService.showMessage(
          'error',
          'Image Required',
          'Please upload an image'
        );
        return;
      }
      reader.readAsDataURL(this.imageFile);
    }
  }

  addbtn(subcategoryForm: NgForm) {

    if (!subcategoryForm.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill in all required fields.');
      return;
    }

    // const input = {
    //   categoryid: Number(this.category),
    //   subcategorycode: this.subcategorycode,
    //   subcategoryname: this.subcategoryname,
    //   subcategorydescription: this.subcategorydescription,
    //   subcategoryorder: this.subcategoryorder,
    //   createdby: this.userid,
    //   status: this.checked ? '1' : '0',
    // };

    const formData = new FormData();

    formData.append('categoryid', String(this.category));
    formData.append('subcategorycode', this.subcategorycode);
    formData.append('subcategoryname', this.subcategoryname);
    formData.append('subcategorydescription', this.subcategorydescription);
    formData.append('subcategoryorder', this.subcategoryorder);

    this.imagejsonvalue = this.imageFileval[0];
    this.photoinfo = this.imageFileval[0].name;
    if (this.imagejsonvalue) {
      formData.append('subcategoryimage', this.imagejsonvalue);
      formData.append('subcategoryimagename', this.photoinfo);
    }
    
    formData.append('createdby', this.userid);
    formData.append('status', this.checked ? '1' : '0');

    let url = GlobalConstants.Authurl + GlobalConstants.Addsubcategory;

    //let url = this.CONFIGSERVICE.getApi('AUTH_URL') + GlobalConstants.Addsubcategory;

    this.dataService.addData(url, formData).subscribe((response: any) => {
      if (response.status == 'success') {

        this.close();
        this.notificationService.showMessage('success', 'subcategory Added', 'The subcategory data has been successfully added!');
      }
      else if (response.status == 'information') {
        this.notificationService.showMessage('error', 'Error', 'The data already exists. Please try again with different information.');
      }
      else {
        this.notificationService.showMessage('error', 'Error', 'There was an issue adding the subcategory data.');
      }
    });
  }

  clear() {

    this.subcategoryname = '';
    this.subcategorycode = '';
    this.username = '';
    this.subcategoryorder = '';
    this.subcategorydescription = '';
    this.checked = false;
    this.calendarValue = null;

    this.imageFile = null;
    this.imagePreview = null;

  }

}
