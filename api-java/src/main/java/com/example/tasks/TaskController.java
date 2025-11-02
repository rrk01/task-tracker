package com.example.tasks;

import org.springframework.web.bind.annotation.*;
import java.util.Map;

@RestController
@RequestMapping("/tasks")
public class TaskController {
    @GetMapping("/health")
    public Map<String, String> health() {
        return Map.of("status", "ok");
    }
}

//sample text