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
  constructor(@Inject(APP_CONFIG) private readonly config: Config) { }

  /**
   * Returns an {@link Observable} which emits a {@link SaveContext} every time
   * it changes according to the backend service.
   */
  public extract(): Observable<SaveContext> {
    return new Observable<string>((sub) => {
      const url = `http://${this.config.apiHost}/api/v1/save-context/stream`;
      const source = new EventSource(url);
  
      function onMessage(evt: MessageEvent) {
        sub.next(evt.data as string);
      }
  
      function onError(evt: Event) {
        sub.error(
          new Error(`SaveContext stream error: ${JSON.stringify(evt)}`),
        );
      }

      source.addEventListener('message', onMessage);
      source.addEventListener('error', onError);

      return () => {
        source.removeEventListener('message', onMessage);
        source.removeEventListener('error', onError);
      };
    }).pipe(
      map((serialized) => {
        const data = JSON.parse(serialized) as SerializedSaveContext;
        return SaveContext.deserialize(data);
      }),
    );
  }
}
