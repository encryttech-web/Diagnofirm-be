// fileupload.ts

import { CommonModule } from '@angular/common';
import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  Output
} from '@angular/core';

import {
  FileUpload,
  FileUploadModule
} from 'primeng/fileupload';

@Component({
  selector: 'app-fileupload',
  standalone: true,
  imports: [
    CommonModule,
    FileUploadModule
  ],
  templateUrl: './fileupload.html',
  styleUrl: './fileupload.scss',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Fileupload {

  // =========================
  // OUTPUTS
  // =========================

  @Output() readonly eventName = new EventEmitter<any>();
  @Output() readonly selectedFileEvent = new EventEmitter<any>();

  // =========================
  // INPUT
  // =========================

  @Input() testdata: any;

  // =========================
  // VARIABLES
  // =========================

  filename: string = '';
  Filecountenable: boolean = true;

  selectedFile: File | null = null;

  filePreviewUrl: string | null = null;

  // =========================
  // FILE SELECT
  // =========================

  onFileSelect(event: any) {

    if (event.files && event.files.length > 0) {

      const file = event.files[0];

      this.selectedFile = file;

      this.filename = file.name;

      this.Filecountenable = false;

      // emit file to parent
      this.eventName.emit(file);

      this.selectedFileEvent.emit(file);

      // preview url
      this.filePreviewUrl = URL.createObjectURL(file);
    }

  }

  // =========================
  // VIEW FILE
  // =========================

  viewFile() {

    if (this.filePreviewUrl) {

      window.open(this.filePreviewUrl, '_blank');

    }

  }

  // =========================
  // CLEAR FILE
  // =========================

  clearFile(fileUploader: FileUpload) {

    this.filename = '';

    this.Filecountenable = true;

    this.selectedFile = null;

    this.filePreviewUrl = null;

    fileUploader.clear();

  }

}