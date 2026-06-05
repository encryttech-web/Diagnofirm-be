import { CommonModule } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  Output
} from '@angular/core';

import { FormsModule, NgForm } from '@angular/forms';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { FileUploadModule } from 'primeng/fileupload';
import { EditorModule } from 'primeng/editor';

import { firstValueFrom, take } from 'rxjs';

import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { CompressImageService } from '@/diagnofirm/services/compress-image.service';

import { Imageupload } from '@/diagnofirm/common/imageupload/imageupload';
import { Imageview } from '@/diagnofirm/common/imageview/imageview';
import { Fileupload } from '@/diagnofirm/common/fileupload/fileupload';

import { HttpService } from '@/layout/service/http.service';

@Component({
  selector: 'app-editnewsletter',
  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    ToggleSwitchModule,
    FileUploadModule,
    Imageupload,
    Imageview,
    EditorModule,
    Fileupload
  ],

  templateUrl: './editnewsletter.html',

  styleUrl: './editnewsletter.scss'
})

export class Editnewsletter {

  // =========================================
  // INPUT OUTPUT
  // =========================================

  @Input()
  editdisplaynewsletter: boolean = false;

  @Input()
  newsletterdata: any;

  @Output()
  editdisplayChange =
    new EventEmitter<boolean>();


  // =========================================
  // BASIC FIELDS
  // =========================================

  newsletterid: number = 0;

  version_no: string = '';

  letter_date: string = '';

  letter_imgname: string = '';

  letter_filename: string = '';

  checked: boolean = true;


  // =========================================
  // IMAGE VARIABLES
  // =========================================

  imageDeleteEnable: boolean = true;

  imageFileval: any[] = [];

  imagejsonvalue: any;

  photoinfo: any;


  // =========================================
  // DOCUMENT VARIABLES
  // =========================================

  fileDeleteEnable: boolean = true;

  documentFileval: any[] = [];

  documentjsonvalue: any;

  documentinfo: any;


  // =========================================
  // IMAGE VIEW
  // =========================================

  visibleImageView: boolean = false;

  images: any = null;

  visible!: boolean;


  // =========================================
  // CONSTRUCTOR
  // =========================================

  constructor(

    private HTTPSERVICE: HttpService,

    private dataService: DataService,

    private notificationService: NotificationService,

    private compressImageService: CompressImageService,

    private cdr: ChangeDetectorRef

  ) { }


  // =========================================
  // INIT
  // =========================================

  ngOnInit() {

    if (this.newsletterdata) {

      this.newsletterid =
        this.newsletterdata.id;

      this.getnewsletterbyId();

    }

  }


  ngOnChanges() {

    if (this.newsletterdata) {

      this.newsletterid =
        this.newsletterdata.id;

      this.getnewsletterbyId();

    }

  }


  // =========================================
  // GET NEWSLETTER
  // =========================================

  getnewsletterbyId() {

    const input = {

      id: Number(this.newsletterid)

    };

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Getnewsletterbyid;

    this.dataService
      .addData(url, input)
      .subscribe((res: any) => {

        if (res.status == 'success') {

          const data =
            res.response.ref1[0];

          this.version_no =
            data.version_no;

          this.letter_date =
            data.letter_date;

          this.letter_imgname =
            data.letter_imgname;

          this.letter_filename =
            data.letter_filename;

          this.checked =
            data.is_active == 1;

          // IMAGE EXIST
          this.imageDeleteEnable =
            !!data.letter_imgname;

          // FILE EXIST
          this.fileDeleteEnable =
            !!data.letter_filename;

          this.cdr.detectChanges();

        }

      });

  }


  // =========================================
  // IMAGE UPLOAD
  // =========================================

  async onImageUpload(event: any) {

    this.imageFileval =
      await this.getCompressedFile(event);

    if (event) {

      this.letter_imgname =
        event.name;

      this.imageDeleteEnable = true;

    }

  }


  // =========================================
  // DOCUMENT UPLOAD
  // =========================================

  async onDocumentUpload(event: any) {

    this.documentFileval = [event];

    if (event) {

      this.letter_filename =
        event.name;

      this.fileDeleteEnable = true;

    }

  }


  // =========================================
  // COMPRESS IMAGE
  // =========================================

  async getCompressedFile(file: any) {

    const arr: any[] = [];

    if (!file) return [];

    const compressed =
      await this.compress(file);

    arr.push(compressed);

    return arr;

  }


  async compress(file: any) {

    return await firstValueFrom(

      this.compressImageService
        .compress(file)
        .pipe(take(1))

    );

  }


  // =========================================
  // IMAGE VIEW
  // =========================================

  imageviwe() {

    this.visibleImageView = false;

    this.images = null;

    this.getImagemasterbyId(
      this.newsletterid
    );

  }


  // =========================================
  // GET IMAGE BY ID
  // =========================================

  getImagemasterbyId(rowid: any) {

    const input = {

      newsletterid: rowid,

      username: '1'

    };

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.GetnewsletterByIdviewImage;

    this.HTTPSERVICE
      .post(url, input)
      .subscribe((response: any) => {

        if (response.status == 'success') {

          const img =
            response.response[0];

          if (!img) return;

          // IMPORTANT
          this.images = {

            imagenamevalue:
              img.imagenamevalue,

            imageBase64value:
              img.imageBase64value

          };

          console.log(this.images);

          // SHOW IMAGE COMPONENT
          this.visibleImageView = true;

          this.cdr.detectChanges();

        }

      });

  }


  // =========================================
  // VIEW FILE
  // =========================================

  viewFile() {

    console.log('View File');

    this.getFileById(this.newsletterid);

  }

  getFileById(rowid: any) {

    const input = {

      newsletterid: rowid,

      username: '1'

    };

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.GetnewsletterByIdviewFile;

    this.HTTPSERVICE
      .post(url, input)
      .subscribe((response: any) => {

        console.log(response);

        if (response.status == 'success') {

          const file =
            response.response[0];

          if (!file) return;

          console.log(file.fileBase64String);

          let base64 =
            file.fileBase64String;

          // REMOVE PREFIX IF EXISTS
          if (base64.includes(',')) {

            base64 =
              base64.split(',')[1];

          }

          // CLEAN SPACES
          base64 =
            base64.replace(/\s/g, '');

          // BASE64 TO BLOB
          const byteCharacters =
            atob(base64);

          const byteNumbers =
            new Array(byteCharacters.length);

          for (let i = 0; i < byteCharacters.length; i++) {

            byteNumbers[i] =
              byteCharacters.charCodeAt(i);

          }

          const byteArray =
            new Uint8Array(byteNumbers);

          const blob = new Blob(
            [byteArray],
            { type: 'application/pdf' }
          );

          const fileURL =
            URL.createObjectURL(blob);

          // DOWNLOAD FILE
          const a =
            document.createElement('a');

          a.href = fileURL;

          a.download =
            this.letter_filename || 'file.pdf';

          document.body.appendChild(a);

          a.click();

          document.body.removeChild(a);

          URL.revokeObjectURL(fileURL);

        }

      });

  }


  // =========================================
  // REMOVE IMAGE
  // =========================================

  removeImage(event: Event) {

    event.stopPropagation();

    this.imageDeleteEnable = false;

    this.letter_imgname = '';

    this.imageFileval = [];

  }


  // =========================================
  // REMOVE DOCUMENT
  // =========================================

  removeDocument(event: Event) {

    event.stopPropagation();

    this.fileDeleteEnable = false;

    this.letter_filename = '';

    this.documentFileval = [];

  }


  // =========================================
  // UPDATE NEWSLETTER
  // =========================================

  editbtn(form: NgForm) {

    if (!form.valid) {

      this.notificationService.showMessage(
        'error',
        'Error',
        'Fill required fields'
      );

      return;

    }

    const formData = new FormData();

    formData.append(
      'nid',
      String(this.newsletterid)
    );

    formData.append(
      'versionno',
      this.version_no
    );

    formData.append(
      'letterdate',
      this.letter_date
    );

    formData.append(
      'isactive',
      this.checked ? '1' : '0'
    );


    // =====================================
    // IMAGE
    // =====================================

    if (
      !this.imageDeleteEnable &&
      this.letter_imgname
    ) {

      formData.append(
        'letterimgname',
        this.letter_imgname
      );

    }
    else if (
      this.imageFileval?.length > 0
    ) {

      this.imagejsonvalue =
        this.imageFileval[0];

      this.photoinfo =
        this.imageFileval[0].name;

      if (this.imageFileval?.length > 0) {
        formData.append('letterimage', this.imageFileval[0]);
      }

      // formData.append(
      //   'letter_imgname',
      //   this.imagejsonvalue
      // );

    }


    // =====================================
    // FILE
    // =====================================

    if (
      !this.fileDeleteEnable &&
      this.letter_filename
    ) {

      formData.append(
        'letterfilename',
        this.letter_filename
      );

    }
    else if (
      this.documentFileval?.length > 0
    ) {

      this.documentjsonvalue =
        this.documentFileval[0];

      this.documentinfo =
        this.documentFileval[0].name;

      if (this.documentFileval?.length > 0) {
        formData.append('letterfile', this.documentFileval[0]);
      }

      // formData.append(
      //   'letter_filename',
      //   this.documentinfo
      // );

    }


    // =====================================
    // API
    // =====================================

    let url =
      GlobalConstants.Authurl +
      GlobalConstants.Updatenewsletter;

    this.dataService
      .addData(url, formData)
      .subscribe((res: any) => {

        if (res.status == 'success') {

          this.close();

          this.notificationService.showMessage(
            'success',
            'Updated',
            'Newsletter updated'
          );

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


  // =========================================
  // CLOSE
  // =========================================

  close() {

    this.editdisplaynewsletter = false;

    this.editdisplayChange.emit(false);

  }


  // =========================================
  // CLEAR
  // =========================================

  clear() {

    this.getnewsletterbyId();

    this.imageFileval = [];

    this.documentFileval = [];

    this.imageDeleteEnable = true;

    this.fileDeleteEnable = true;

  }

}