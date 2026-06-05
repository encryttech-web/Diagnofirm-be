import { Fileupload } from '@/diagnofirm/common/fileupload/fileupload';
import { Imageupload } from '@/diagnofirm/common/imageupload/imageupload';
import { CompressImageService } from '@/diagnofirm/services/compress-image.service';
import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule } from 'primeng/fileupload';
import { InputTextModule } from 'primeng/inputtext';
import { firstValueFrom, take } from 'rxjs';

@Component({
  selector: 'app-addnewsletter',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    EditorModule,
    Imageupload,
    FileUploadModule,
    Fileupload
  ],
  templateUrl: './addnewsletter.html',
  styleUrl: './addnewsletter.scss'
})
export class Addnewsletter {

  @Input() display: boolean = false;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() dataReloaded = new EventEmitter<any>();

  // =========================
  // NEWSLETTER FIELDS
  // =========================

  usr_id: string = '';
  version_no: string = '';
  letter_date: string = '';
  letter_ord: string = '';
  is_active: string = '1';

  letter_image: File | null = null;
  letter_imgname: string = '';

  letter_file: any = null;      // base64/compressed image
  letter_filename: string = '';

  created_by: string = '';

  // image upload
  imageFileval: any[] = [];
  imagejsonvalue: any;

  selectedFile: File | null = null;

  // letter_file: any = null;

  // letter_filename: string = '';

  constructor(
    private dataService: DataService,
    private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private COMPRESSIMAGESERVICE: CompressImageService
  ) { }

  ngOnInit() {
    this.CDR.detectChanges();
  }

  // =========================
  // IMAGE HANDLER 
  // =========================

  async someMethod(event: any) {
    this.imageFileval = await this.gettingFile(event);
  }

  async gettingFile(imagefile: any) {
    const arr: any[] = [];
    if (!imagefile) return arr;

    const compressed = await this.compressFile(imagefile);
    arr.push(compressed);
    return arr;
  }

  async compressFile(file: any) {
    const result = await firstValueFrom(
      this.COMPRESSIMAGESERVICE.compress(file).pipe(take(1))
    );
    return result;
  }

  // =========================
  // FILE HANDLER 
  // =========================


  onFileUpload(event: any) {

    if (event) {

      this.selectedFile = event;

      // actual file
      this.letter_file = event;

      // filename
      this.letter_filename = event.name;

    }

  }

  // =========================
  // ADD NEWSLETTER
  // =========================

  addbtn(form: NgForm) {

    if (!form.valid) {
      this.notificationService.showMessage(
        'error',
        'Validation',
        'Please fill required fields'
      );
      return;
    }

    const formData = new FormData();

    formData.append('usr_id', this.usr_id);
    formData.append('version_no', this.version_no);
    formData.append('letter_date', this.letter_date);
    

    // =========================
    // IMAGE SAVE
    // =========================

    if (
      this.imageFileval &&
      this.imageFileval.length > 0
    ) {

      this.imagejsonvalue =
        this.imageFileval[0];

      this.letter_imgname =
        this.imageFileval[0].name;

      formData.append(
        'letter_image',
        this.imagejsonvalue
      );

      formData.append(
        'letter_imgname',
        this.letter_imgname
      );

    }


    // =========================
    // FILE SAVE
    // =========================

    if (this.letter_file) {

      formData.append(
        'letter_file',
        this.letter_file,
        this.letter_filename
      );

      formData.append(
        'letter_filename',
        this.letter_filename
      );

    }

    formData.append('letter_ord', this.letter_ord);
    formData.append('is_active', this.is_active);
    formData.append('created_by', this.created_by);

    // // image file
    // if (this.imageFileval && this.imageFileval.length > 0) {
    //   this.imagejsonvalue = this.imageFileval[0];
    //   this.letter_imgname = this.imageFileval[0].name;

    //   formData.append('letter_image', this.imagejsonvalue);
    //   formData.append('letter_imgname', this.letter_imgname);
    // }

    // // editor content / file data
    // formData.append('letter_file', this.letter_file || '');
    // formData.append('letter_filename', this.letter_filename || '');

   

    let url = GlobalConstants.Authurl + GlobalConstants.Addnewsletter;

    this.dataService.addData(url, formData).subscribe((response: any) => {

      if (response.status === 'success') {

        this.close();

        this.notificationService.showMessage(
          'success',
          'Success',
          'Newsletter added successfully'
        );

      } else {

        this.notificationService.showMessage(
          'error',
          'Error',
          response.message || 'Failed to add newsletter'
        );

      }

    });
  }

  // =========================
  // CLOSE
  // =========================

  close() {
    this.display = false;
    this.displayChange.emit(this.display);
    this.dataReloaded.emit();
  }

  // =========================
  // CLEAR
  // =========================

  clear() {
    this.usr_id = '';
    this.version_no = '';
    this.letter_date = '';
    this.letter_ord = '';
    this.is_active = '1';

    this.letter_file = null;
    this.letter_filename = '';
    this.imageFileval = [];
  }
}