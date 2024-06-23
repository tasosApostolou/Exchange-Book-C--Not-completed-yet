import { DialogRef } from '@angular/cdk/dialog';
import { Component } from '@angular/core';

@Component({
  selector: 'app-buy-book',
  standalone: true,
  imports: [],
  templateUrl: './buy-book.component.html',
  styles: [
    `
      :host {
        display: block;
        background: #fff;
        border-radius: 8px;
        padding: 16px;
        width:800px;
        .inlineimage{max-width:470px;margin-right: 8px;margin-left: 10px}.images{display: inline-block;max-width: 98%;height: auto;width: 22%;margin: 1%;left:20px;text-align: center}
      }
      .a{
        background:lightGreen;
      }
    `,
  ],})
export class BuyBookComponent {
  constructor(public dialogPassword: DialogRef) {}


}
