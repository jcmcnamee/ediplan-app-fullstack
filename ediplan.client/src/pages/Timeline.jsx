import { TimelineProvider } from '../features/timeline/TimelineContext';
import TimelineWindow from '../features/timeline/TimelineWindow';

function Timeline() {
  const mockBooking = {
    startDate: new Date(2024, 7, 2),
    endDate: new Date(2024, 8, 2),
  };

  const currentDate = new Date();
  console.log(currentDate);

  return (
    <TimelineProvider>
      <div>Timeline page</div>
      <TimelineWindow booking={mockBooking} />
    </TimelineProvider>
  );
}

export default Timeline;
