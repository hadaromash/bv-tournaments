import React from "react";
import logo from "./logo.png";
import styled from "styled-components";

const Logo = () => (
  <Container>
    <Round />
    <ImageContainer>
      <Image src={logo} alt="logo" />
    </ImageContainer>
  </Container>
);

export default Logo;

const Container = styled.div`
  align-self: center;
  width: 200px;
  height: 200px;
  position: relative;
`;

const Content = styled.div`
  width: 100%;
  height: 100%;
  position: absolute;
  top: 0;
  left: 0;
`;

const Round = styled(Content)`
  left: 30px;
  height: 140px;
  width: 140px;
  background-color: #e2edf4;
  border-radius: 70px;
`;

const ImageContainer = styled(Content)`
  z-index: 2;
`;

const Image = styled.img`
  max-width: 100%;
  height: auto;
`;
