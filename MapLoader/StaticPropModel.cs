using System;
using System.Collections.Generic;
using System.Text;
using Source.Bsp;
using Source.Bsp.LumpData.GameLumps;
using Source.Mdl;

namespace Source.MapLoader
{
    public class StaticPropModel: Model
    {
        public StaticPropModel(StaticProp prop, List<string> models, ResourceManager resourceManager)
        {
            this.Position = prop.Origin;
            var modelName = models[prop.PropType];
            var model = resourceManager.Get<SourceModel>(modelName.Replace('\\', '/').ToLower().Trim());
            //TODO use information of model to read actual geometry from vtx files
        }

        public static List<StaticPropModel> ReadProps(Map map, ResourceManager manager)
        {
            var staticProps = new List<StaticPropModel>();
            var game = map.Lumps.GetGame();
            var bspStaticProps = game.GetStaticProps();
            foreach (var prop in bspStaticProps.Props)
            {
                staticProps.Add(new StaticPropModel(prop, bspStaticProps.Models, manager));
            }
            return staticProps;
        }
    }
}
