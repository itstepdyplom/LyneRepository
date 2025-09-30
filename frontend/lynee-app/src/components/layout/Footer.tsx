'use client';

import React, { useState } from 'react';
import {
  Box,
  Typography,
  Link,
  Divider,
  Select,
  MenuItem,
  FormControl,
  SelectChangeEvent,
} from '@mui/material';
import {
  Facebook,
  Instagram,
  Pinterest,
  YouTube,
} from '@mui/icons-material';
import { useTranslations } from 'next-intl';
import { useParams, useRouter } from 'next/navigation';

const Footer: React.FC = () => {
  const t = useTranslations('Footer');
  const params = useParams();
  const router = useRouter();
  const currentLocale = params.locale as string;
  
  const [selectedLanguage, setSelectedLanguage] = useState(() => {
    if (currentLocale === 'uk') return 'Ukraine - Ukrainian';
    if (currentLocale === 'en') return 'United Kingdom - English';
    return 'United Kingdom - English';
  });

  const handleLanguageChange = (event: SelectChangeEvent) => {
    const newValue = event.target.value;
    setSelectedLanguage(newValue);
    
    let newLocale = 'uk';
    if (newValue === 'United Kingdom - English') {
      newLocale = 'en';
    }
    
    // Отримуємо поточний шлях без локалі
    const currentPath = window.location.pathname;
    const pathWithoutLocale = currentPath.replace(/^\/(uk|en)/, '');
    
    // Перенаправляємо на нову мову з тим же шляхом
    router.push(`/${newLocale}${pathWithoutLocale}`);
  };

  const footerSections = {
    [t('theCompany')]: [
      t('about'),
      t('designAndCraft'),
      t('press'),
    ],
    [t('assistance')]: [
      t('deliveryInfo'),
      t('payments'),
      t('returns'),
      t('faq'),
      t('productCare'),
      t('sizeGuide'),
      t('fitGuide'),
      t('studentDiscount'),
    ],
    [t('legal')]: [
      t('privacyPolicy'),
      t('termsConditions'),
      t('cookieNotice'),
      t('accessibility'),
    ],
  };

  const socialLinks = [
    { icon: Facebook, label: 'Facebook', href: '#' },
    { icon: Instagram, label: 'Instagram', href: '#' },
    { icon: Pinterest, label: 'Pinterest', href: '#' },
    { icon: YouTube, label: 'TikTok', href: '#' },
  ];

  return (
    <Box
      component="footer"
      sx={{
        backgroundColor: '#000000',
        color: 'white',
        width: '100vw',
        position: 'relative',
        left: '50%',
        right: '50%',
        marginLeft: '-50vw',
        marginRight: '-50vw',
        pt: 8,
        pb: 0,
        mt: 8,
      }}
    >
      <Box
        sx={{
          mb: 8,
          height: 300,
          backgroundImage: 'url(https://images.unsplash.com/photo-1441986300917-64674bd600d8?ixlib=rb-4.0.3&auto=format&fit=crop&w=1200&q=80)',
          backgroundSize: 'cover',
          backgroundPosition: 'center',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          position: 'relative',
          mx: 0,
          '&::before': {
            content: '""',
            position: 'absolute',
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            backgroundColor: 'rgba(0, 0, 0, 0.4)',
          },
        }}
      >
        <Box
          sx={{
            textAlign: 'center',
            zIndex: 1,
            color: 'white',
          }}
        >
          <Typography
            variant="h2"
            sx={{
              fontWeight: 300,
              letterSpacing: '0.2em',
              mb: 1,
              fontSize: { xs: '2rem', md: '3rem' },
            }}
          >
            LYNEE
          </Typography>
          <Typography
            variant="subtitle1"
            sx={{
              letterSpacing: '0.3em',
              fontSize: '0.875rem',
              fontWeight: 300,
            }}
          >
            CONCEPT STORE
          </Typography>
        </Box>
      </Box>

      <Box sx={{ mb: 6, px: { xs: 4, md: 8 } }}>
        <Box
          sx={{
            display: 'flex',
            flexDirection: { xs: 'column', md: 'row' },
            gap: { xs: 4, md: 8 },
            justifyContent: 'space-between',
            maxWidth: '1200px',
            mx: 'auto',
          }}
        >
          {Object.entries(footerSections).map(([title, links]) => (
            <Box
              key={title}
              sx={{
                flex: title === 'ASSISTANCE' ? 2 : 1,
                minWidth: { xs: '100%', md: '200px' },
              }}
            >
              <Typography
                variant="h6"
                sx={{
                  fontWeight: 400,
                  letterSpacing: '0.1em',
                  mb: 3,
                  fontSize: '0.875rem',
                  color: 'white',
                }}
              >
                {title}
              </Typography>
              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                {links.map((link) => (
                  <Link
                    key={link}
                    href="#"
                    sx={{
                      color: 'rgba(255, 255, 255, 0.7)',
                      textDecoration: 'none',
                      fontSize: '0.875rem',
                      transition: 'color 0.3s ease',
                      '&:hover': {
                        color: 'white',
                      },
                    }}
                  >
                    {link}
                  </Link>
                ))}
              </Box>
            </Box>
          ))}

          <Box
            sx={{
              minWidth: { xs: '100%', md: '200px' },
            }}
          >
            <Typography
              variant="h6"
              sx={{
                fontWeight: 400,
                letterSpacing: '0.1em',
                mb: 3,
                fontSize: '0.875rem',
                color: 'white',
              }}
            >
              {t('followUs')}
            </Typography>
            <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
              {socialLinks.map(({ icon: Icon, label, href }) => (
                <Box
                  key={label}
                  sx={{
                    display: 'flex',
                    alignItems: 'center',
                    gap: 1,
                  }}
                >
                  <Icon sx={{ fontSize: '1.2rem', color: 'rgba(255, 255, 255, 0.7)' }} />
                  <Link
                    href={href}
                    sx={{
                      color: 'rgba(255, 255, 255, 0.7)',
                      textDecoration: 'none',
                      fontSize: '0.875rem',
                      transition: 'color 0.3s ease',
                      '&:hover': {
                        color: 'white',
                      },
                    }}
                  >
                    {label}
                  </Link>
                </Box>
              ))}
            </Box>
          </Box>
        </Box>
      </Box>

      <Box sx={{ px: { xs: 4, md: 8 } }}>
        <Divider sx={{ borderColor: 'rgba(255, 255, 255, 0.2)', mb: 4, maxWidth: '1200px', mx: 'auto' }} />
      </Box>

      <Box
        sx={{
          px: { xs: 4, md: 8 },
          pb: 4,
        }}
      >
        <Box
          sx={{
            display: 'flex',
            flexDirection: { xs: 'column', md: 'row' },
            justifyContent: 'space-between',
            alignItems: { xs: 'flex-start', md: 'center' },
            gap: 3,
            maxWidth: '1200px',
            mx: 'auto',
          }}
        >
          <Box
            sx={{
              display: 'flex',
              alignItems: 'center',
              gap: 2,
            }}
          >
            <Typography
              variant="body2"
              sx={{
                color: 'rgba(255, 255, 255, 0.7)',
                fontSize: '0.875rem',
              }}
            >
              {t('changeLocation')}:
            </Typography>
            <FormControl
              size="small"
              sx={{
                minWidth: 200,
                '& .MuiOutlinedInput-root': {
                  color: 'white',
                  '& fieldset': {
                    borderColor: 'rgba(255, 255, 255, 0.3)',
                  },
                  '&:hover fieldset': {
                    borderColor: 'rgba(255, 255, 255, 0.5)',
                  },
                  '&.Mui-focused fieldset': {
                    borderColor: 'white',
                  },
                },
                '& .MuiSelect-icon': {
                  color: 'white',
                },
              }}
            >
              <Select
                value={selectedLanguage}
                displayEmpty
                onChange={handleLanguageChange}
                sx={{
                  color: 'white',
                  fontSize: '0.875rem',
                }}
              >
                <MenuItem value="United Kingdom - English">
                  United Kingdom - English
                </MenuItem>
                <MenuItem value="Ukraine - Ukrainian">
                  Ukraine - Ukrainian
                </MenuItem>
              </Select>
            </FormControl>
          </Box>
        </Box>
      </Box>

        <Box
            sx={{
                backgroundColor: 'white',
                color: 'black',
                textAlign: 'center',
                height: '70px',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'start',
                pl: 8,
            }}
            >
            <Typography
                variant="body2"
                sx={{
                fontSize: '1.2rem',
                letterSpacing: '0.05em',
                fontWeight: 400,
                }}
            >
                {t('copyright', { year: new Date().getFullYear() })}
            </Typography>
        </Box>
    </Box>
  );
};

export default Footer; 