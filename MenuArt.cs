using System;

namespace BreakTimer
{
    class MenuArt
        //Class to hold menu art
    {
        public string[] art = new[]
        {
            @"###                                                                              #####  ",
            @" #   ####     # #####  #####  #####  ######   ##   #    #    #   # ###### ##### #     # ",
            @" #  #         #   #    #    # #    # #       #  #  #   #      # #  #        #         # ",
            @" #   ####     #   #    #####  #    # #####  #    # ####        #   #####    #      ###  ",
            @" #       #    #   #    #    # #####  #      ###### #  #        #   #        #      #    ",
            @" #  #    #    #   #    #    # #   #  #      #    # #   #       #   #        #           ",
            @"###  ####     #   #    #####  #    # ###### #    # #    #      #   ######   #      #    ",
        };

        public void ArtPrinter()
        {
            Console.WindowWidth = 100;
            Console.WriteLine("\n\n");
            foreach (var line in art)
            {
                Console.WriteLine(line);
            }
        }
    }
}