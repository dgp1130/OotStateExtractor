import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { APP_CONFIG, Config } from './app.config';
import { SaveContext, SerializedSaveContext } from './models/save_context';

@Injectable({
  providedIn: 'root'
})
export class ExtractorService {
  constructor(
    @Inject(APP_CONFIG) private readonly config: Config,
    private readonly http: HttpClient,
  ) { }

  public extract(): Observable<SaveContext> {
    const url = `http://${this.config.apiHost}/api/v1/save-context`;
    return this.http.get(url).pipe(
      map((res) => SaveContext.deserialize(res as SerializedSaveContext)),
    );
  }
}
