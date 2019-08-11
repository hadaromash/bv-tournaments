import React from "react";

const Share = ({ number, text, children, handleClick }) => {
  var href = "https://wa.me/";
  if (number) {
    href += number;
  }

  if (text) {
    href += "?text=" + encodeURI(text);
  }
  return (
    <a target="_blank" rel="noopener noreferrer" href={href} onClick={handleClick}>
      {children}
    </a>
  );
};

export default Share;
