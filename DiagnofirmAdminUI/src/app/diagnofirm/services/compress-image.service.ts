import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompressImageService {

  private readonly MAX_SIZE = 2048;
  private readonly QUALITY = 0.7;

  compress(file: File): Observable<File> {

    return new Observable(observer => {

      const reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = (ev: ProgressEvent<FileReader>) => {

        // ✅ SAFE NULL CHECK (FIX for TS18047)
        const result = ev.target?.result;
        if (!result) {
          observer.error('File read failed');
          return;
        }

        const img = new Image();

        img.src = result.toString();

        // IMPORTANT: wait for image load
        img.onload = () => {

          // ✅ SAFE NULL GUARD (fix TS errors)
          if (!img) {
            observer.error('Image not created');
            return;
          }

          const canvas = document.createElement('canvas');
          const ctx = canvas.getContext('2d');

          // extra safety
          if (!ctx) {
            observer.error('Canvas context not found');
            return;
          }

          const width = img.naturalWidth || img.width;
          const height = img.naturalHeight || img.height;

          if (!width || !height) {
            observer.error('Invalid image size');
            return;
          }

          // compression ratio
          const ratio = this.MAX_SIZE / Math.max(width, height);

          canvas.width = width * (ratio > 1 ? 1 : ratio);
          canvas.height = height * (ratio > 1 ? 1 : ratio);

          ctx.drawImage(img, 0, 0, canvas.width, canvas.height);

          canvas.toBlob(blob => {

            if (!blob) {
              observer.error('Compression failed');
              return;
            }

            const compressedFile = new File(
              [blob],
              file.name,
              {
                type: file.type || 'image/jpeg',
                lastModified: Date.now()
              }
            );

            observer.next(compressedFile);
            observer.complete();

          }, file.type || 'image/jpeg', this.QUALITY);
        };

        // image load error safety
        img.onerror = () => {
          observer.error('Image load error');
        };
      };

      reader.onerror = err => observer.error(err);
    });
  }

  // OPTIONAL: Base64 converter (if needed)
  toBase64(file: File): Observable<string> {
    return new Observable(observer => {
      const reader = new FileReader();

      reader.readAsDataURL(file);

      reader.onload = (e: ProgressEvent<FileReader>) => {
        const result = e.target?.result;

        if (!result) {
          observer.error('Base64 conversion failed');
          return;
        }

        observer.next(result.toString());
        observer.complete();
      };

      reader.onerror = err => observer.error(err);
    });
  }
}