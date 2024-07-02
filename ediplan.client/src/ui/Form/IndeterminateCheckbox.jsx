import { useEffect, useRef } from 'react';

function IndeterminateCheckbox({ indeterminate, ...rest }) {
  const ref = useRef(null);

  useEffect(() => {
    if (typeof indeterminate === 'boolean') {
      ref.current.indeterminate = !rest.checked && indeterminate;
    }
  }, [ref, indeterminate, rest.checked]);

  return <input type="checkbox" ref={ref} {...rest} />;
}

export default IndeterminateCheckbox;
