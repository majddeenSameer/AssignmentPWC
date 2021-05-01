import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
import { merge } from 'rxjs';
import { filter, map, switchMap } from 'rxjs/operators';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { Keyboard } from '@ionic-native/keyboard/ngx';

import { environment } from '@env/environment';
import { Logger, UntilDestroy, untilDestroyed } from '@core';

const log = new Logger('App');

@UntilDestroy()
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  textDir: string = 'ltr';

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private titleService: Title,
    private translateService: TranslateService,
    private zone: NgZone,
    private keyboard: Keyboard,
    private statusBar: StatusBar,
    private splashScreen: SplashScreen,
  ) {
    this.translateService.onLangChange.subscribe((event: LangChangeEvent) => {
      if (event.lang == 'ar-AR') {
        this.textDir = 'rtl';
      } else {
        this.textDir = 'ltr';
      }
    });
  }

  async ngOnInit() {
    // Setup logger
    if (environment.production) {
      Logger.enableProductionMode();
    }

    log.debug('init');

    // Setup translations

    const onNavigationEnd = this.router.events.pipe(filter((event) => event instanceof NavigationEnd));

    // Change page title on navigation or language change, based on route data
    merge(this.translateService.onLangChange, onNavigationEnd)
      .pipe(
        map(() => {
          let route = this.activatedRoute;
          while (route.firstChild) {
            route = route.firstChild;
          }
          return route;
        }),
        filter((route) => route.outlet === 'primary'),
        switchMap((route) => route.data),
        untilDestroyed(this)
      )
      .subscribe((event) => {
        const title = event.title;
        if (title) {
          this.titleService.setTitle(this.translateService.instant(title));
        }
      });
    // Cordova platform and plugins initialization
    document.addEventListener(
      'deviceready',
      () => {
        this.zone.run(() => this.onCordovaReady());
      },
      false
    );
  }

  ngOnDestroy() {
  }

  private onCordovaReady() {
    log.debug('device ready');

    if ((window as any).cordova) {
      log.debug('Cordova init');

      this.keyboard.hideFormAccessoryBar(true);
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    }
  }
}
