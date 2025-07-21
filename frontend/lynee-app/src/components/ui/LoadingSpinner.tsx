'use client';

import React from 'react';
import { Box, CircularProgress, Typography } from '@mui/material';

interface LoadingSpinnerProps {
  size?: number;
  message?: string;
  fullScreen?: boolean;
}

const LoadingSpinner: React.FC<LoadingSpinnerProps> = ({
  size = 40,
  message = 'Loading...',
  fullScreen = false,
}) => {
  const content = (
    <>
      <CircularProgress 
        size={size} 
        sx={{ 
          color: 'primary.main',
          mb: message ? 2 : 0 
        }} 
      />
      {message && (
        <Typography 
          variant="body2" 
          color="text.secondary"
          sx={{ 
            textAlign: 'center',
            letterSpacing: '0.05em'
          }}
        >
          {message}
        </Typography>
      )}
    </>
  );

  if (fullScreen) {
    return (
      <Box
        sx={{
          position: 'fixed',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          justifyContent: 'center',
          backgroundColor: 'rgba(255, 255, 255, 0.9)',
          zIndex: 9999,
        }}
      >
        {content}
      </Box>
    );
  }

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        py: 4,
      }}
    >
      {content}
    </Box>
  );
};

export default LoadingSpinner; 