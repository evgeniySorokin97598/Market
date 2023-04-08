import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, InjectionToken, Optional } from '@angular/core';
import { catchError, filter, map, mapTo, Observable, of, ReplaySubject, tap } from 'rxjs';

 


export const ENVIRONMENT = new InjectionToken<{ [key: string]: any }>('environment');
 
export class ConfigService {
  private readonly environment: any;
    
  // We need @Optional to be able start app without providing environment file
  constructor(@Optional() @Inject(ENVIRONMENT) environment: any) {
    this.environment = environment !== null ? environment : {};
  }

  getValue(key: string, defaultValue?: any): any {
    return this.environment[key] || defaultValue;
  }
}
@Injectable()
export class ConfigurationService {
  private configurationSubject = new ReplaySubject<any>(1);

  constructor(private httpClient: HttpClient) {
            this.load();
 
  }

  // method can be used to refresh configuration
  load(): void {
    this.httpClient.get('/assets/config.json')
      .pipe(
        catchError(() => of(null)),
        filter(Boolean),
      )
      .subscribe((configuration: any) => this.configurationSubject.next(configuration));
  }

  getValue(key: string, defaultValue?: any): Observable<any> {
     
    return this.configurationSubject
      .pipe(
        map((configuration: any) => configuration[key] || defaultValue),
      );
  }
  
}

 