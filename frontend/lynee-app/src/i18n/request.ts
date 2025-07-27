import { getRequestConfig } from 'next-intl/server';

const locales = ['en', 'uk'];

export default getRequestConfig(async ({ locale }) => {
  if (!locale || !locales.includes(locale)) {
    return {
      locale: 'uk',
      messages: (await import(`../../messages/uk.json`)).default
    };
  }

  return {
    locale,
    messages: (await import(`../../messages/${locale}.json`)).default
  };
}); 