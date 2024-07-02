import { useEffect, useRef } from "react";

export function useOutsideClick(handler, listenOnCapture = true) {
  const ref = useRef();

  useEffect(
    function () {
      function handleClick(e) {
        if (ref.current && !ref.current.contains(e.target)) {
          handler();
        }
      }
      // add listener on capture
      document.addEventListener("click", handleClick, listenOnCapture);

      return () => {
        document.removeEventListener("click", handleClick, listenOnCapture);
      };
    },
    [handler, listenOnCapture]
  );

  return ref;
}
