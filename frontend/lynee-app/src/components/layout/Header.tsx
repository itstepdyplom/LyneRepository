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

const Header: React.FC = () => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [searchOpen, setSearchOpen] = useState(false);
  
  const { isAuthenticated, user, logout } = useAuthStore();
  const { totalItems, openCart } = useCartStore();


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
                  primary={`Welcome, ${user?.name}`}
                  primaryTypographyProps={{ variant: 'body2' }}
                />
              </ListItem>
              <ListItem disablePadding>
                <ListItemButton onClick={logout}>
                  <ListItemText
                    primary="Logout"
                    primaryTypographyProps={{ variant: 'subtitle2' }}
                  />
                </ListItemButton>
              </ListItem>
            </>
          ) : (
            <>
              <ListItem disablePadding>
                <ListItemButton component={Link} href="/auth/login">
                  <ListItemText
                    primary="Login"
                    primaryTypographyProps={{ variant: 'subtitle2' }}
                  />
                </ListItemButton>
              </ListItem>
              <ListItem disablePadding>
                <ListItemButton component={Link} href="/auth/register">
                  <ListItemText
                    primary="Register"
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
              href="/"
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
            <IconButton onClick={handleSearchToggle}>
              <SearchIcon />
            </IconButton>
            
            <IconButton component={Link} href="/account">
              <PersonIcon />
            </IconButton>
            
            <IconButton onClick={openCart}>
              <Badge badgeContent={totalItems} color="primary">
                <ShoppingBag />
              </Badge>
            </IconButton>
          </Box>
        </Toolbar>

        {searchOpen && (
          <Box
            sx={{
              backgroundColor: (theme) => alpha(theme.palette.background.paper, 0.95),
              borderTop: '1px solid',
              borderColor: 'divider',
              p: 2,
            }}
          >
            <InputBase
              placeholder="Search luxury items..."
              fullWidth
              autoFocus
              sx={{
                fontSize: '1.1rem',
                '& input': {
                  textAlign: 'center',
                  py: 1,
                },
              }}
            />
          </Box>
        )}

      </AppBar>

      {mobileMenu}
    </>
  );
};

export default Header; 