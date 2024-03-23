namespace View;

public static class UserMessages
{
    private const string RunSimulatorQuestionMessage = "Would you like to run the simulator?";
    private const string RunningSimulatorMessage = "Simulating...";

    private const string WelcomeMessage = """
                                                    :::::::::: :::          :::::::: :::::::::::   :::   :::   :::    ::: :::            ::: ::::::::::: ::::::::  :::::::::
                                                :+:      :+:+:         :+:    :+:    :+:      :+:+: :+:+:  :+:    :+: :+:          :+: :+:   :+:    :+:    :+: :+:    :+:
                                               +:+        +:+         +:+           +:+     +:+ +:+:+ +:+ +:+    +:+ +:+         +:+   +:+  +:+    +:+    +:+ +:+    +:+
                                              :#::+::#   +#+         +#++:++#++    +#+     +#+  +:+  +#+ +#+    +:+ +#+        +#++:++#++: +#+    +#+    +:+ +#++:++#:
                                             +#+        +#+                +#+    +#+     +#+       +#+ +#+    +#+ +#+        +#+     +#+ +#+    +#+    +#+ +#+    +#+
                                            #+#        #+#         #+#    #+#    #+#     #+#       #+# #+#    #+# #+#        #+#     #+# #+#    #+#    #+# #+#    #+#
                                           ###      #######        ######## ########### ###       ###  ########  ########## ###     ### ###     ########  ###    ###
                                           """;

    private const string ExitMessage = "Thank you for running the simulation";

    private const string SeparatorMessage = "*****************************";

    public static void Welcome() {
        PrintMessage(WelcomeMessage);
    }

    public static void Exit() {
        PrintMessage(ExitMessage);
    }

    public static void Simulate() {
        PrintMessage(RunningSimulatorMessage);
    }

    public static void RunSimulatorQuestion() {
        PrintMessage(RunSimulatorQuestionMessage);
    }
    
    private static void PrintMessage(string message) {
        Console.WriteLine(SeparatorMessage);
        Console.WriteLine(message);
        Console.WriteLine(SeparatorMessage);
    }
}