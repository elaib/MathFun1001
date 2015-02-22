﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathFun1000 {
    public class Tutorial : IProblemType{

        public String[] step;
        public String[] example;
        public String[] rule;
        public int difficulty = 1;
        public int number_of_steps = 5;
        public int current_step = 0;

        //Basic Constructor
        public Tutorial() 
        {
            this.number_of_steps = 5;
            this.difficulty = 1;
            this.step = new String[] {"Identify the different variables.", 
                "Separate the variables into like groups.", 
                "Combine the like terms.", 
                "Recombine the terms after removing the parenthese to make an equation in its seimplets form. Since the variables are differnt, this is the simplest form.", 
                "Solution"};
            this.example = new String[] {"(2x<sup>2</sup>y -3xy) + (6x<sup>2</sup>y-9xy)", 
                "(2x<sup>2</sup>y+6x<sup>2</sup>y) + (-3xy-9xy)", 
                "(8x<sup>2</sup>y) + (-12xy)", 
                "8x<sup>2</sup>y-12xy", 
                "8x<sup>2</sup>y-12xy"};
            this.rule = new String[] {"Rule Here", 
                "Rule Here", 
                "Rule Here", 
                "Rule Here", 
                "Rule Here"};
        }

        public Tutorial(String[] step, String[] example, String[] rule, int difficulty, int number_of_steps)
        {
            this.step = step;
            this.example = example;
            this.rule = rule;
            this.difficulty = difficulty;
            this.number_of_steps = number_of_steps;
        }

        public override string generateCode(int numOfSteps)
        {
            string code = "";
            if (numOfSteps > -1)
                for (int i = 0; i <= numOfSteps; i++)
                {
                    if (i < number_of_steps)
                    {
                        code += "<div class=\"StepContainer\">";

                        code += "<div class=\"box\">" + getStepAt(i) + "</div>";
                        code += "<div class=\"box\">" + getExampleAt(i) + "</div>";
                        code += "<div class=\"box\">" + getRuleAt(i) + "</div>";

                        code += "<div class=\"buttons\">";
                        code += "</div>";

                        code += "</div>";
                    }
                }

            return code;
            
        }

        public void incrementStep()
        {
            this.current_step++;
        }

        public void decrementStep()
        {
            this.current_step--;
        }

        public override int getNumberOfSteps()
        {
            return number_of_steps;
        }

        public override string getExampleAt(int index)
        {
            return example[index];
        }

        public override string getStepAt(int index)
        {
            return step[index];
        }

        public override string getRuleAt(int index)
        {
            return rule[index];
        }
    }
}