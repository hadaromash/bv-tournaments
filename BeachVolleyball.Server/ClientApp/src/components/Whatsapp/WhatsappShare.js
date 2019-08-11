import React from "react";

const Share = ({ number, text, children }) => {
  var href = "https://wa.me/";
  if (number) {
    href += number;
  }

  if (text) {
    href += "?text=" + encodeURI(text);
  }
  return (
    <a target="_blank" rel="noopener noreferrer" href={href}>
      {children}
    </a>
  );
};

export default Share;
