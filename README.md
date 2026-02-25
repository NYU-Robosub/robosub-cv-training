# RoboSub Perception — Synthetic CV Training Pipeline

A synthetic computer vision training pipeline for the NYU RoboSub team. This project uses **Unity Perception** to generate labeled underwater imagery in simulation, then converts and trains object-detection models (YOLO / COCO formats) for real-time inference on the submarine's vision stack.

## Key Capabilities

- **Synthetic data generation** — Unity scene with domain-randomized lighting, textures, camera poses, and underwater conditions.
- **Automated labeling** — Bounding-box and semantic-segmentation labels exported directly from Unity Perception.
- **Format conversion** — Scripts to convert raw Perception output to COCO and YOLO annotation formats.
- **Training & evaluation** — Ready-made train/eval scripts with configurable model architectures, augmentations, and metrics.
- **ONNX export** — Export trained models for deployment on the sub's inference hardware.

## Project Structure

```
robosub-cv-training/
├── unity/
│   ├── RobosubPerceptionSim/          # Full Unity project
│   │   ├── Assets/
│   │   │   ├── Scenes/
│   │   │   │   └── MainScene.unity
│   │   │   ├── Scripts/
│   │   │   │   ├── Randomizers/
│   │   │   │   └── Labelers/
│   │   │   ├── Prefabs/
│   │   │   └── Materials/
│   │   ├── ProjectSettings/
│   │   └── Packages/
│   └── perception-config/
│       ├── camera_config.json
│       └── labelers.json
├── data/
│   ├── raw/                           # Direct exports from Perception
│   │   ├── run_0001/
│   │   └── run_0002/
│   ├── processed/                     # Converted for training
│   │   ├── coco/
│   │   └── yolo/
│   ├── samples/                       # Small dataset for quick tests
│   └── export_scripts/
│       ├── convert_to_coco.py
│       └── convert_to_yolo.py
├── training/
│   ├── models/
│   │   └── architecture_defs.py       # Model definitions
│   ├── notebooks/
│   │   └── exploration.ipynb
│   ├── train.py                       # Train script
│   ├── eval.py                        # Evaluation script
│   ├── requirements.txt
│   └── utils/
│       ├── dataset_loader.py
│       ├── augmentations.py
│       └── metrics.py
├── inference/
│   ├── export_onnx.py                 # Export pipeline
│   └── onnx_models/                   # Models ready for inference
├── tests/
│   ├── data_pipeline_tests.py
│   └── training_tests.py
├── .gitignore
├── CONTRIBUTING.md
├── LICENSE
└── README.md                          # ← You are here
```

## Quick Start

1. **Generate data** — Open the Unity project in `unity/RobosubPerceptionSim/`, configure randomizers, and run the Perception scenario.
2. **Convert annotations** — Run `data/export_scripts/convert_to_coco.py` or `convert_to_yolo.py` to produce training-ready datasets.
3. **Train** — `python training/train.py` (see `docs/training_guidelines.md` for details).
4. **Evaluate** — `python training/eval.py` to compute metrics on a held-out set.
5. **Export** — `python inference/export_onnx.py` to produce an ONNX model for deployment.

## License

See [LICENSE](LICENSE) for details.