using System;
using System.Threading.Tasks;
using AutoMapper;
using sibintek.sibmobile.core;

namespace sibintek.team
{
    public class CreateUserCommand : BaseCommand
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
    }

    [CommandHandler("conversation:create-user")]
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly UserService userService;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(UserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task Handle(CreateUserCommand command)
        {
            var user = mapper.Map<User>(command);
            await userService.CreateOrUpdateAsync(user);
        }
    }
}