import { useQuery } from '@tanstack/react-query';

// Define a function to fetch data from the API
const fetchData = async () => {
  try {
    // Make the GET request to the API endpoint
    const response = await fetch('http://localhost:3000');

    // Check if the response is successful (status code 200-299)
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }

    // Parse the response as JSON
    const data = await response.json();

    // Return the data
    return data;
  } catch (error) {
    // Handle any errors that occurred during the fetch
    throw new Error(`Error fetching data: ${error.message}`);
  }
};

function Dashboard() {
  const { data, isLoading, error } = useQuery({
    queryKey: ['test'],
    queryFn: fetchData,
  });

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  console.log(data);

  return (
    <div>
      <h1>Hello from React!</h1>
      <h2>{data}</h2>
    </div>
  );
}

export default Dashboard;
