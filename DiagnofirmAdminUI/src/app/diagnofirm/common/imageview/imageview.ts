import { Component, Input, Output, EventEmitter, OnChanges, ChangeDetectorRef } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { ImageModule } from 'primeng/image';

@Component({
  selector: 'app-imageview',
  standalone: true,
  imports: [DialogModule, ImageModule],
  templateUrl: './imageview.html',
  styleUrls: ['./imageview.scss']
})
export class Imageview implements OnChanges {

  @Input() images: any;
  @Input() visible: boolean = false;

  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() remove = new EventEmitter<void>();

  imgValue: any;
  zoomImage: string | null = null;

  constructor(
    private CDR: ChangeDetectorRef,
  ) { }

  ngOnInit(): void {
  }

  ngOnChanges(): void {
    this.imgValue = this.images;
    this.CDR.detectChanges();
  }

  btncls() {
    this.visible = false;
    this.visibleChange.emit(false);
  }

  handleDialogHide() {
    this.visible = false;
    this.visibleChange.emit(false);
  }

  myFunction(event: any) {
    this.zoomImage = event.target.src;
  }

  handleLiveDemoChange(event: any) {
    this.visible = event;
  }
}