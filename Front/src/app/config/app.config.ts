import { API_CONFIG, getApiUrl } from '../constants/api.constants';

export const APP_CONFIG = {
  API_BASE_URL: getApiUrl(false),
  TIMEOUT: API_CONFIG.TIMEOUT,
  RETRY_ATTEMPTS: API_CONFIG.RETRY_ATTEMPTS
} as const;