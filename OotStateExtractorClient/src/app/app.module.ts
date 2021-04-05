import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { APP_CONFIG, config } from './app.config';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
  ],
  providers: [{
    provide: APP_CONFIG,
    useValue: config,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
