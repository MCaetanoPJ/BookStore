import { getApiUrl } from '../app/constants/api.constants';

export const environment = {
  production: true,
  apiUrl: getApiUrl(true)
};