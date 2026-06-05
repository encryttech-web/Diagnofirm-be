import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class VideoCompressionService {

  constructor() {}

  // Compress video by resizing it using HTML5 Canvas
  async compressVideo(file: File, targetWidth: number = 640, targetHeight: number = 360): Promise<Blob> {
    return new Promise((resolve, reject) => {
      const video = document.createElement('video');
      const canvas = document.createElement('canvas');
      const ctx = canvas.getContext('2d');

      // Ensure that canvas and video elements are available
      if (!ctx) {
        reject('Canvas context not available');
        return;
      }

      const fileReader = new FileReader();

      fileReader.onload = () => {
        // Load the video file into the video element
        video.src = fileReader.result as string;

        video.onloadedmetadata = () => {
          // Set the canvas size to the desired target size
          canvas.width = targetWidth;
          canvas.height = targetHeight;

          // Play the video
          video.play();

          // Once the video starts playing, draw frames on the canvas
          video.currentTime = 0;

          video.onplay = () => {
            // Draw the first frame on the canvas
            ctx.drawImage(video, 0, 0, targetWidth, targetHeight);

            // Convert canvas to Blob (image data)
            canvas.toBlob(
              (blob) => {
                if (blob) {
                  resolve(blob);  // Return the compressed video as a Blob
                } else {
                  reject('Error generating compressed video');
                }
              },
              'video/mp4',  // Output format
              0.5  // Quality (0.5 is a mid-level quality)
            );
          };
        };
      };

      fileReader.onerror = (error) => {
        reject(error);
      };

      fileReader.readAsDataURL(file); // Read the file as a data URL
    });
  }
}
