import { getApiUrl } from '../app/constants/api.constants';

export const environment = {
  production: false,
  apiUrl: getApiUrl(false)
};