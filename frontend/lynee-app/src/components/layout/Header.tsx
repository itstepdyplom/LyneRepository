'use client';

import React, { useState } from 'react';
import {
  AppBar,
  Toolbar,
  Typography,
  Box,
  IconButton,
  Badge,
  Drawer,
  List,
  ListItem,
  ListItemText,
  ListItemButton,
  useMediaQuery,
  useTheme,
  InputBase,
  alpha,
} from '@mui/material';
import {
  Menu as MenuIcon,
  ShoppingBag,
  Search as SearchIcon,
  Person as PersonIcon,
  Close as CloseIcon,
} from '@mui/icons-material';
import { useAuthStore } from '../../stores/authStore';
import { useCartStore } from '../../stores/cartStore';
import Link from 'next/link';
import { useTranslations } from 'next-intl';
import { useParams } from 'next/navigation';

const Header: React.FC = () => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [searchOpen, setSearchOpen] = useState(false);
  
  const { isAuthenticated, user, logout } = useAuthStore();
  const { totalItems, openCart } = useCartStore();
  const t = useTranslations('Header');
  const params = useParams();
  const locale = params.locale as string;

  const handleDrawerToggle = () => {
    setMobileMenuOpen(!mobileMenuOpen);
  };

  const handleSearchToggle = () => {
    setSearchOpen(!searchOpen);
  };

  const mobileMenu = (
    <Drawer
      anchor="left"
      open={mobileMenuOpen}
      onClose={handleDrawerToggle}
      sx={{
        '& .MuiDrawer-paper': {
          width: 280,
          backgroundColor: 'background.default',
          padding: 2,
        },
      }}
    >
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          mb: 3,
        }}
      >
        <Typography variant="h6" sx={{ fontWeight: 300, letterSpacing: '0.1em' }}>
          LYNEE
        </Typography>
        <IconButton onClick={handleDrawerToggle}>
          <CloseIcon />
        </IconButton>
      </Box>
      
      <List>
      
        
        <Box sx={{ mt: 3, pt: 3, borderTop: '1px solid', borderColor: 'divider' }}>
          {isAuthenticated ? (
            <>
              <ListItem>
                <ListItemText
                  primary={t('welcome', { name: user?.name || 'User' })}
                  primaryTypographyProps={{ variant: 'body2' }}
                />
              </ListItem>
              <ListItem disablePadding>
                <ListItemButton onClick={logout}>
                  <ListItemText
                    primary={t('logout')}
                    primaryTypographyProps={{ variant: 'subtitle2' }}
                  />
                </ListItemButton>
              </ListItem>
            </>
          ) : (
            <>
              <ListItem disablePadding>
                <ListItemButton component={Link} href={`/${locale}/auth/login`}>
                  <ListItemText
                    primary={t('login')}
                    primaryTypographyProps={{ variant: 'subtitle2' }}
                  />
                </ListItemButton>
              </ListItem>
              <ListItem disablePadding>
                <ListItemButton component={Link} href={`/${locale}/auth/register`}>
                  <ListItemText
                    primary={t('register')}
                    primaryTypographyProps={{ variant: 'subtitle2' }}
                  />
                </ListItemButton>
              </ListItem>
            </>
          )}
        </Box>
      </List>
    </Drawer>
  );

  return (
    <>
      <AppBar position="sticky" elevation={0} sx={{ backgroundColor: 'white', color: 'black' }}>
        <Toolbar sx={{ justifyContent: 'space-between', px: { xs: 2, md: 4 } }}>
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <IconButton
              edge="start"
              onClick={handleDrawerToggle}
              sx={{ mr: 2 }}
            >
              <MenuIcon />
            </IconButton>
          </Box>

          {/* Center - Logo */}
          <Box sx={{ position: 'absolute', left: '50%', transform: 'translateX(-50%)' }}>
            <Typography
              variant="h4"
              component={Link}
              href={`/${locale}`}
              sx={{
                fontWeight: 300,
                letterSpacing: '0.2em',
                textDecoration: 'none',
                color: 'inherit',
                fontSize: { xs: '1.5rem', md: '2rem' },
              }}
            >
              LYNE
            </Typography>
          </Box>

          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            {searchOpen ? (
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                <InputBase
                  placeholder={t('search')}
                  autoFocus
                  sx={{
                    fontSize: '1rem',
                    backgroundColor: 'white',
                    borderRadius: 1,
                    px: 2,
                    py: 1,
                    border: '1px solid',
                    borderColor: 'divider',
                    minWidth: 200,
                    '& input': {
                      py: 0.5,
                    },
                  }}
                />
                <IconButton onClick={handleSearchToggle} size="small">
                  <CloseIcon />
                </IconButton>
              </Box>
            ) : (
              <IconButton onClick={handleSearchToggle}>
                <SearchIcon />
              </IconButton>
            )}
            
            <IconButton component={Link} href={`/${locale}/account`}>
              <PersonIcon />
            </IconButton>
            
            <IconButton component={Link} href={`/${locale}/cart`}>
              <Badge badgeContent={totalItems} color="primary">
                <ShoppingBag />
              </Badge>
            </IconButton>
          </Box>
        </Toolbar>
      </AppBar>

      {mobileMenu}
    </>
  );
};

export default Header; 