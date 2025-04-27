import { useMutation, useQueryClient } from '@tanstack/react-query';
import toast from 'react-hot-toast';
import { createBooking } from '../../services/apiBookings';

export function useCreateBooking() {
  const queryClient = useQueryClient();

  const { mutate: apiCreateBooking, isPending: isCreating } = useMutation({
    mutationFn: data => createBooking(data),
    onSuccess: () => {
      toast.success('New booking successfully created.');
      queryClient.invalidateQueries({ queryKey: ['bookings'] });
    },
    onError: err => {
      const { [""]: messages } = err.response.data.errors;
      for (var message of messages) {
        toast.error(message);
      }
    }
  });

  return { isCreating, apiCreateBooking };
}
