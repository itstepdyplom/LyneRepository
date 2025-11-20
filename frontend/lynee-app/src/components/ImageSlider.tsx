import { useState, useEffect } from "react";
import { Box } from "@mui/material";
import Image from "next/image";

interface ImageSliderProps {
  images: string[];
}

export default function ImageSlider({ images }: ImageSliderProps) {
  const [index, setIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setIndex((prev) => (prev + 1) % images.length);
    }, 3000);

    return () => clearInterval(interval);
  }, [images.length]);

  return (
    <Box
      sx={{
        height: { xs: '35vh', sm: '45vh', md: '60vh' },
        position: "relative",
        borderRadius: "12px",
      }}
    >
      <Image
        src={images[index]}
        alt={`image-${index}`}
        fill
        style={{ objectFit: "contain" }}
      />

      <Box
        sx={{
          position: "absolute",
          right: { xs: 10, md: 30, lg: 60 },
          bottom: { xs: 20, sm: 30, md: 40 },
          pr: 1,
          display: "flex",
          gap: { xs: 1, sm: 1.2, md: 1.5 },
        }}
      >
        {images.map((_, i) => {
          const active = index === i;
          const distance = Math.abs(index - i);

          let scale = 1;
          if (distance === 0) scale = 1.4;
          else if (distance === 1) scale = 1.2;

          return (
            <Box
              key={i}
              onClick={() => setIndex(i)}
              sx={{
                width: { xs: 24, sm: 28, md: 32 },
                height: { xs: 24, sm: 28, md: 32 },
                borderRadius: "50%",
                backgroundColor: active ? "white" : "rgba(255,255,255,0.6)",
                color: active ? "black" : "rgba(0,0,0,0.5)",
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                fontSize: { xs: "12px", sm: "13px", md: "15px" },
                fontWeight: 600,
                cursor: "pointer",
                transition: "all 0.25s ease",
                boxShadow: active
                  ? "0px 2px 6px rgba(0,0,0,0.15)"
                  : "0px 1px 3px rgba(0,0,0,0.1)",

                "&:hover": {
                  backgroundColor: "white",
                  color: "black",
                  border: "2px solid black",
                },
                transform: {
                  xs: `scale(${scale * 0.85})`,
                  sm: `scale(${scale * 0.95})`,
                  md: `scale(${scale})`,
                },
              }}
            >
              {i + 1}
            </Box>
          );
        })}
      </Box>
    </Box>
  );
}
