import { Directive, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { FileUpload } from 'primeng/fileupload';

@Directive({
  selector: 'p-fileUpload[formControlName], p-fileUpload[formControl]',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileUploadControlValueAccessor),
      multi: true
    }
  ]
})
export class FileUploadControlValueAccessor implements ControlValueAccessor {
    private onChange?: (value: any) => void;  
    private onTouched?: () => void;  

  constructor(private fileUpload: FileUpload) {}

  writeValue(value: any): void {
    if (value) {
      this.fileUpload.clear(); 
    }
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.fileUpload.disabled = isDisabled;  
  }

  onFileSelect(event: any) {
    this.onChange!(event.files);
  }
}
