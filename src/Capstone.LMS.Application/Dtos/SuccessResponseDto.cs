namespace Capstone.LMS.Application.Dtos
{
    public class SuccessResponseDto
    {
        public bool Succeeded { get; private set; }

        public void Success() => Succeeded = true;
        public void Failure() => Succeeded = false;
    }
}
