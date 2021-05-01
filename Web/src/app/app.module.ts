import { HttpClientModule } from '@angular/common/http';
import { Injector, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthModule } from '@app/auth';
import { CoreModule } from '@core';
import { Keyboard } from '@ionic-native/keyboard/ngx';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@shared';
import { NgWizardConfig, NgWizardModule, THEME } from 'ng-wizard';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxPermissionsModule } from 'ngx-permissions';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LocatorService } from './services/locator.service';


const ngWizardConfig: NgWizardConfig = {
  theme: THEME.dots,
};

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    TranslateModule.forRoot(),
    NgbModule,
    CoreModule,
    SharedModule,
    AuthModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    AccordionModule.forRoot(),
    NgWizardModule.forRoot(ngWizardConfig),
    AppRoutingModule, // must be imported as the last module as it contains the fallback route
    NgxPermissionsModule.forRoot()
  ],
  declarations: [AppComponent],
  providers: [Keyboard, StatusBar, SplashScreen, BsModalService],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor(injector: Injector) {
    LocatorService.injector = injector;
  }
}
