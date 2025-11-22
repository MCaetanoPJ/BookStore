import { Injectable } from '@angular/core';
import { APP_CONFIG } from '../config/app.config';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  getApiUrl(): string {
    return APP_CONFIG.API_BASE_URL;
  }

  getTimeout(): number {
    return APP_CONFIG.TIMEOUT;
  }

  getRetryAttempts(): number {
    return APP_CONFIG.RETRY_ATTEMPTS;
  }
}