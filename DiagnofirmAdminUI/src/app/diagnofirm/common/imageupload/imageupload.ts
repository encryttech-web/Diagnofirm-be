import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FileUpload, FileUploadModule } from 'primeng/fileupload';

@Component({
  selector: 'app-imageupload',
  standalone: true,
  imports: [CommonModule, FileUploadModule],
  templateUrl: './imageupload.html',
  styleUrls: ['./imageupload.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Imageupload {

  @Output() readonly eventName = new EventEmitter<any>();
  @Output() readonly imageFile = new EventEmitter<any>();

  @Input() testdata: any;

  filename: string = '';
  Filecountenable: boolean = true;

  imagePreview: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;

  onFileSelect(event: any) {
    if (event.files?.length > 0) {
      const file = event.files[0];

      this.selectedFile = file;
      this.filename = file.name;
      this.Filecountenable = false;

      this.eventName.emit(file);
      this.imageFile.emit(file);

      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result;
      };
      reader.readAsDataURL(file);
    }
  }

  clearFile(fileUploader: FileUpload) {
    this.filename = '';
    this.Filecountenable = true;
    this.selectedFile = null;
    this.imagePreview = null;

    fileUploader.clear();
  }
}